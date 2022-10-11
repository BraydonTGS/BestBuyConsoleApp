using System;
using BestBuyPro.Products;
using static System.Console;
namespace BestBuyPro.Printing
{
    public static class PrintingText
    {

        // Program Title //
        public static void PrintTitle()
        {
            Clear();
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine("------------------------------------------------------------------------");
            WriteLine(@" +-+-+-+-+-+-+-+ +-+-+-+-+-+-+-+ +-+-+-+-+
 |P|r|o|d|u|c|t| |M|a|n|a|g|e|r| |3|0|0|0|
 +-+-+-+-+-+-+-+ +-+-+-+-+-+-+-+ +-+-+-+-+");
            WriteLine("------------------------------------------------------------------------");
            ForegroundColor = previousColor;
        }

        // Search Results //
        public static void SearchResults()
        {
            Clear();
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine("------------------------------------------------------------------------");
            WriteLine(@" +-+-+-+-+-+-+ +-+-+-+-+-+-+-+
 |S|e|a|r|c|h| |R|e|s|u|l|t|s|
 +-+-+-+-+-+-+ +-+-+-+-+-+-+-+");
            WriteLine("------------------------------------------------------------------------");
            ForegroundColor = previousColor;
        }

        // Printing a Custom Menu //
        public static int PrintCustomMenu(string prompt, string[] options)
        {
            MenuPrinter selection = new MenuPrinter(prompt, options);
            int index = selection.Run();
            return index;
        }

        // Press Enter to Continue //
        public static void Continue()
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            Write("\n> Press Any Key To Continue... ");
            ReadKey();
            WriteLine();
            ForegroundColor = previousColor;
        }

        // Loading //
        public static void Loading()
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            Write("\n> Loading Please Wait");
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(650);
                Write(".");
            }
            WriteLine();
            ForegroundColor = previousColor;
        }

        //Display Products //
        public static void DisplayProducts(IEnumerable<Product> products)
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            foreach (var product in products)
            {
                WriteLine("------------------------------------------------------------------------");
                WriteLine($"> ProductId: {product.ProductId}\n> Name: {product.Name}\n> Price: {product.Price:C2}\n> CategoryId: {product.CategoryId}\n> OnSale: {product.OnSale}\n> Stock Level: {product.StockLevel}");
                WriteLine("------------------------------------------------------------------------");
                Thread.Sleep(100);
            }

            ForegroundColor = previousColor;
        }

        // Display Product //
        public static void DisplayProduct(Product product)
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine("------------------------------------------------------------------------\n");
            WriteLine($"----------> {product.Name} <---------------\n");
            WriteLine($"> Product ID: {product.ProductId}\n");
            WriteLine($"> Product Price: {product.Price:C2}\n");
            WriteLine($"> Category ID: {product.CategoryId}\n");
            WriteLine($"> Number On Sale: {product.OnSale}\n");
            WriteLine($"> Product Stock Level: {product.CategoryId}\n");
            WriteLine("------------------------------------------------------------------------");
            ForegroundColor = previousColor;
        }

        // Invalid Selection //
        public static void InvalidSelection()
        {
            PrintTitle();
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine("------------------------------------------------------------------------");
            WriteLine("\n> PLEASE ENTER A VALID SELECTION <\n");
            WriteLine("------------------------------------------------------------------------");
            ForegroundColor = previousColor;

        }

        // Product Not Found //
        public static void ProductNotFound()
        {
            PrintTitle();
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine("------------------------------------------------------------------------");
            WriteLine("\n> ERROR: PRODUCT NOT FOUND <\n");
            WriteLine("------------------------------------------------------------------------");
            ForegroundColor = previousColor;
        }


        // Exit the Program //
        public static void Exit()
        {
            string exit = "\n> Thank you.\n\n> Please Press Any Key To Exit: ";
            PrintCharacters(exit);
        }

        // Method for Printing one character at at time //
        public static void PrintCharacters(string input)
        {
            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < input.Length; i++)
            {
                Write(input[i]);
                Thread.Sleep(65);

                // Skip to the End of the String //
                if (KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Write(input.Substring(i + 1));
                        break;
                    }
                }
            }
            WriteLine();
            ForegroundColor = previousColor;
        }

    }
}

