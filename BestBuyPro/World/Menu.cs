using System;
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
            string[] options = { "Display Products", "Search for a Product", "Add a New Product", "Update an Existing Product", "Delete a Product", "Exit" };
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
                PrintingText.PrintTitle();
                Write("\n> Please Enter the Product Price: ");
                var parse = double.TryParse(ReadLine(), out double price);
                if (!parse)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                PrintingText.PrintTitle();
                Write("\n> Please Enter a CategoryId (1-10): ");
                var parse2 = int.TryParse(ReadLine(), out int categoryId);
                if (!parse2)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                string prompt1 = "Is the Product on Sale";
                string[] options1 = { "Yes", "No" };
                int sale = 0;
                var userChoice = PrintingText.PrintCustomMenu(prompt1, options1);
                if (userChoice == 0)
                {
                    sale = 1;
                }
                if (userChoice == 1)
                {
                    sale = 0;
                }
                PrintingText.PrintTitle();
                Write("\n> Please Enter the Item Stock Quantity: ");
                var parse4 = int.TryParse(ReadLine(), out int stock);
                if (!parse4)
                {
                    PrintingText.InvalidSelection();
                    AddAProduct();
                }
                var stockString = Convert.ToString(stock);
                Repo.InsertProduct(name, price, categoryId, sale, stockString);
                var products = Repo.GetProducts();
                var product = products.FirstOrDefault(pro => pro.Name == name);
                PrintingText.Loading();
                PrintingText.PrintSuccess();
                PrintingText.Continue();
                PrintingText.PrintTitle();
                PrintingText.DisplayProduct(product);
                PrintingText.Continue();
                ProductInfoMenu();
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
                    UpdateAProduct();
                }
                var products = Repo.GetProducts();
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
                    PrintingText.PrintTitle();
                    WriteLine($"\nPlease Update the Product with the Product Id: {productID} \n");
                    Write("> Please Enter a New Product Name: ");
                    var name = ReadLine().Trim() ?? "Default";
                    PrintingText.PrintTitle();
                    Write("\n> Please Enter the New Product Price: ");
                    var parse1 = double.TryParse(ReadLine(), out double price);
                    if (!parse1)
                    {
                        PrintingText.InvalidSelection();
                        AddAProduct();
                    }
                    PrintingText.PrintTitle();
                    Write("\n> Please Enter a New CategoryId (1-10): ");
                    var parse2 = int.TryParse(ReadLine(), out int categoryId);
                    if (!parse2)
                    {
                        PrintingText.InvalidSelection();
                        AddAProduct();
                    }
                    string prompt1 = "Is the Product Still on Sale";
                    string[] options1 = { "Yes", "No" };
                    int sale = 0;
                    var userChoice = PrintingText.PrintCustomMenu(prompt1, options1);
                    if (userChoice == 0)
                    {
                        sale = 1;
                    }
                    if (userChoice == 1)
                    {
                        sale = 0;
                    }
                    PrintingText.PrintTitle();
                    Write("\n> Please Enter the New Item Stock Quantity: ");
                    var parse4 = int.TryParse(ReadLine(), out int stock);
                    if (!parse4)
                    {
                        PrintingText.InvalidSelection();
                        AddAProduct();
                    }
                    var stockString = Convert.ToString(stock);
                    Repo.UpdateProduct(productID, name, price, categoryId, sale, stockString);
                    var products1 = Repo.GetProducts();
                    var product1 = products.FirstOrDefault(pro => pro.Name == name);
                    PrintingText.Loading();
                    PrintingText.PrintSuccess();
                    PrintingText.Continue();
                    PrintingText.PrintTitle();
                    PrintingText.DisplayProduct(product);
                    PrintingText.Continue();
                    PrintingText.PrintTitle();
                    string prompt2 = "Would you like update more Products?";
                    string[] options2 = { "Yes", "No" };
                    var userSelection = PrintingText.PrintCustomMenu(prompt2, options2);
                    if (userSelection == 0)
                    {
                        UpdateAProduct();
                    }
                    if (userSelection == 1)
                    {
                        PrintingText.Loading();
                        ProductInfoMenu();
                    }
                    ForegroundColor = previousColor;
                }
            }
        }

        // Delete a Product //
        public void DeleteAProduct()
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
                    PrintingText.Loading();
                    DeleteAProduct();
                }
                var products = Repo.GetProducts();
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
                    PrintingText.PrintTitle();
                    string prompt1 = $"Are you sure you want to DELETE the Product with the Product Id: {productID} ";
                    string[] options1 = { "Yes", "No" };
                    var userChoice = PrintingText.PrintCustomMenu(prompt1, options1);
                    if (userChoice == 0)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        PrintingText.Caution();
                        Write($"\nTYPE DELETE TO DELETE PRODUCT {productID} FROM THE DATABASE \n");
                        WriteLine($"\nTYPE CANCEL TO GO BACK TO THE MAIN MENU \n");

                        var deleteOrNot = ReadLine().ToUpper().Trim() ?? "ERROR";

                        if (deleteOrNot == "DELETE")
                        {
                            PrintingText.Loading();
                            Repo.DeleteProduct(productID);
                            PrintingText.PrintSuccess();
                            PrintingText.Continue();
                            ProductInfoMenu();

                        }
                        else if (deleteOrNot == "CANCEL")
                        {
                            PrintingText.Loading();
                            PrintingText.PrintTitle();
                            PrintingText.Continue();
                            ProductInfoMenu();
                        }

                        else
                        {
                            PrintingText.InvalidSelection();
                            PrintingText.Continue();
                            PrintingText.Loading();
                            ProductInfoMenu();
                        }
                        ForegroundColor = previousColor;
                        Console.WriteLine(deleteOrNot);
                        ReadKey();


                    }
                    if (userChoice == 1)
                    {
                        PrintingText.Loading();
                        ProductInfoMenu();
                    }

                }
            }
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

