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
                WriteLine($"> ProductId: {product.ProductId}\n> Name: {product.Name}\n> Price: {product.Price}\n> CategoryId: {product.CategoryId}\n> OnSale: {product.OnSale}\n> Stock Level: {product.StockLevel}");
                WriteLine("------------------------------------------------------------------------");
                Thread.Sleep(100);
            }

            ForegroundColor = previousColor;
        }


        // Exit the Game //
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

