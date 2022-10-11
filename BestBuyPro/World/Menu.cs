﻿using System;
using System.Data;
using System.Data.Common;
using System.Net.NetworkInformation;
using BestBuyPro.Connections;
using BestBuyPro.Printing;
using BestBuyPro.Products;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using static System.Console;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BestBuyPro.Menu
{
    public class Menu
    {
        private BBConnection NewConnection;
        private DapperProductRepository Repo;

        public Menu()
        {
            NewConnection = new BBConnection();
            Repo = new DapperProductRepository(NewConnection.GetConnection());
        }

        // Start of the Program //
        public void Run()
        {

            Title = "Best Buy Product Manager 3000";
            //PrintingText.Loading();
            //PrintingText.PrintTitle();
            //PrintingText.Continue();
            ProductInfoMenu();

        }

        // Employee Login //
        public void LogIn()
        {
            // Search for the employeeID number //
        }

        // Product Information Menu//
        public void ProductInfoMenu()
        {
            PrintingText.PrintTitle();
            string prompt = "Please Select From the Following Options: ";
            string[] options = { "Display Products", "Search for a Product", "Add a New Product", "Delete a Product", "Exit" };
            var menuIndex = PrintingText.PrintCustomMenu(prompt, options);
            switch (menuIndex)
            {
                case 0:
                    DisplayProducts();
                    break;
                case 1:
                    SearchForProduct();
                    break;
                case 2:
                    AddAProduct();
                    break;
                case 3:
                    UpdateAProduct();
                    break;
                case 4:
                    DeleteAProduct();
                    break;
                case 5:
                    ExitGame();
                    break;
                default:
                    break;
            }
        }

        // Display Products //
        public void DisplayProducts()
        {
            PrintingText.Loading();
            PrintingText.PrintTitle();
            var products = Repo.GetProducts();
            PrintingText.DisplayProducts(products);
            PrintingText.Continue();
            ProductInfoMenu();

        }

        // Search for a Product //
        public void SearchForProduct()
        {
            PrintingText.Loading();
            PrintingText.PrintTitle();
            int productID = 0;
            bool confirmed = false;
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            while (!confirmed)
            {
                PrintingText.PrintTitle();
                Write("\n> Please Enter a Product Identification Number: ");
                bool parse = int.TryParse(ReadLine(), out int num);
                if (!parse)
                {
                    PrintingText.InvalidSelection();
                    PrintingText.Continue();
                    PrintingText.Loading();
                    ProductInfoMenu();
                }

                WriteLine($"\n> You entered * {num} *\n");
                string prompt = "Use This Product Identification Number? ";
                string[] options = { "Yes", "No" };
                var userResponse = PrintingText.PrintCustomMenu(prompt, options);
                if (userResponse == 0)
                {
                    PrintingText.Loading();
                    productID = num;
                    confirmed = true;
                }
                if (userResponse == 1)
                {
                    SearchForProduct();
                }
            }
            var products = Repo.SearchForProduct(productID);
            // Using Linq to grab the product from the collection based on prodId //
            var product = products.FirstOrDefault(pro => pro.ProductId == Convert.ToString(productID));
            if (product == null)
            {
                PrintingText.ProductNotFound();
                PrintingText.Continue();
                PrintingText.Loading();
                ProductInfoMenu();
            }
            else
            {
                PrintingText.SearchResults();
                PrintingText.DisplayProduct(product);
                PrintingText.Continue();
                PrintingText.PrintTitle();
                string prompt = "Would you like to search for more Products?";
                string[] options = { "Yes", "No" };
                var userSelection = PrintingText.PrintCustomMenu(prompt, options);
                if (userSelection == 0)
                {
                    SearchForProduct();
                }
                if (userSelection == 1)
                {
                    PrintingText.Loading();
                    ProductInfoMenu();
                }
                ForegroundColor = previousColor;
            }
        }

        // Add a new Product //
        public void AddAProduct()
        {
            PrintingText.Loading();
            PrintingText.PrintTitle();
            string prompt = "Would You like to Add a New Product to the Database? ";
            string[] options = { "Yes", "No" };
            var userSelection = PrintingText.PrintCustomMenu(prompt, options);
            if (userSelection == 0)
            {
                ConsoleColor previousColor = ForegroundColor;
                ForegroundColor = ConsoleColor.Green;
                PrintingText.Loading();
                PrintingText.PrintTitle();
                WriteLine("\nPlease Add a New Product to the Best Buy DataBase\n");
                Write("> Please Enter a Product Name: ");
                var name = ReadLine().Trim() ?? "Default";
                Write("\n> Please Enter the  Product Price: ");
                var parse = double.TryParse(ReadLine(), out double price);
                if (!parse)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                Write("\n> Please Enter a CategoryId (1-10): ");
                var parse2 = int.TryParse(ReadLine(), out int categoryId);
                if (!parse2)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                Write("\n> Is the Product on Sale? ");
                var parse3 = int.TryParse(ReadLine(), out int sale);
                if (!parse3)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                Write("\n> Please Enter the Item Stock Amount: ");
                var parse4 = int.TryParse(ReadLine(), out int stock);
                if (!parse4)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                string prompt1 = "Is the Product on Sale";
                string[] options1 = { "Yes", "No" };
                var userChoice = PrintingText.PrintCustomMenu(prompt1, options1);
                if (userChoice == 0)
                {

                }
                if (userChoice == 1)
                {

                }
                var stockString = Convert.ToString(stock);
                WriteLine("Dont press enter");
                ReadKey();
                Repo.InsertProduct(name, price, categoryId, sale, stockString);
                var products = Repo.GetProducts();
                var product = products.FirstOrDefault(pro => pro.Name == name);
                PrintingText.Loading();
                PrintingText.PrintTitle();
                WriteLine("\n> Success!!!");
                PrintingText.Continue();
                PrintingText.PrintTitle();
                PrintingText.DisplayProduct(product);
                ReadKey();

                ForegroundColor = previousColor;
            }
            else
            {
                PrintingText.Loading();
                PrintingText.Continue();
                ProductInfoMenu();
            }

        }

        // Update a Product //
        public void UpdateAProduct()
        {

        }


        // Delete a Product //
        public void DeleteAProduct()
        {
            WriteLine("Test");
            PrintingText.Continue();
            ProductInfoMenu();
        }

        // Exit //
        public void ExitGame()
        {
            PrintingText.PrintTitle();
            PrintingText.Exit();
            ReadKey();
            PrintingText.Loading();
            Environment.Exit(0);
        }
    }
}

