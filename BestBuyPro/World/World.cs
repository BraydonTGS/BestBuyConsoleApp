using System;
using BestBuyPro.Printing;
using static System.Console;
namespace BestBuyPro.World
{
    public class World
    {
        public World()
        {
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
            PrintingText.PrintCustomMenu(prompt, options);
            ReadKey();

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

