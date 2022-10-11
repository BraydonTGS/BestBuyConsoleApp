using System;
using System.Data;

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

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}

