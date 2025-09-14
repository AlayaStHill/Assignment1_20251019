using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        IEnumerable<Product> GetAllProducts();
    }
}