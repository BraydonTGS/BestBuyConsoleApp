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

        public IEnumerable<Product> SearchForProduct(int prodID)
        {
            return _connection.Query<Product>("SELECT * FROM Products p WHERE p.ProductID = @prodID", new { prodID = prodID });
        }

    }
}

