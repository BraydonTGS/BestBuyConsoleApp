using System;
namespace BestBuyPro.Products
{
    public class Product
    {
        // Matching the Column Names in MySql //
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int OnSale { get; set; }
        public string StockLevel { get; set; }

        public Product()
        {
        }
    }
}

