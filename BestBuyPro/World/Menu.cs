using System;
using System.Data;
using System.Data.Common;
using BestBuyPro.Connections;
using BestBuyPro.Printing;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using static System.Console;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BestBuyPro.World
{
    public class Menu
    {
        private BBConnection NewConnection;

        public Menu()
        {
            NewConnection = new BBConnection();
        }

        // Start of the Program //
        public void Run()
        {

            Title = "Best Buy Product Manager 3000";
            PrintingText.Loading();
            PrintingText.PrintTitle();
            PrintingText.Continue();
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
            string prompt = "> Please Select From the Following Options: ";
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
                    DeleteAProduct();
                    break;
                case 4:
                    ExitGame();
                    break;
                default:
                    break;


            }
            ReadKey();
        }

        // Display Products //
        public void DisplayProducts()
        {

        }

        // Search for a Product //
        public void SearchForProduct()
        {

        }
        // Add a new Product //
        public void AddAProduct()
        {

        }

        // Delete a Product //
        public void DeleteAProduct()
        {

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

