using System;
namespace BestBuyPro.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }
}

