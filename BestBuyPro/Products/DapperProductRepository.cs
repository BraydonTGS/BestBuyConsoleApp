using System;
using System.Data;
using Dapper;

namespace BestBuyPro.Products
{
    public class DapperProductRepository : IProductRepository
    {
        // Value Will Never Change //
        private readonly IDbConnection _connection;


        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        // List All Products - Limit 50 //
        public IEnumerable<Product> GetProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products p ORDER BY p.ProductId DESC LIMIT 50;");
        }


        // Search DB for a Specific Product // 
        public IEnumerable<Product> SearchForProduct(int prodID)
        {
            return _connection.Query<Product>("SELECT * FROM Products p WHERE p.ProductID = @prodID", new { prodID = prodID });
        }


        // Insert a New Product //
        public void InsertProduct(string name, double price, int categoryId, int sale, string stock)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@name, @price, @categoryID, @sale, @stock);",
            new { name = name, price = price, categoryId = categoryId, sale = sale, stock = stock });

        }


        // Update an Existing Product //
        public void UpdateProduct(int prodId, string name, double price, int categoryId, int sale, string stock)
        {
            _connection.Execute("UPDATE Products SET Name = @name, Price = @price, CategoryID = @categoryId, OnSale = @sale, StockLevel = @stock WHERE ProductID = @prodId;", new { name = name, price = price, categoryId = categoryId, sale = sale, stock = stock, prodId = prodId, });
        }


        // Delete a Product //
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }





    }
}

