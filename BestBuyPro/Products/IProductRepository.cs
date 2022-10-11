using System;
namespace BestBuyPro.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void InsertProduct(string name, double price, int categoryId, int sale, string stock);
        void UpdateProduct(int prodId, string name, double price, int categoryId, int sale, string stock);
        void DeleteProduct(int productID)
    }
}

