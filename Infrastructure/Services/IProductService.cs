using Infrastructure.Models;

namespace Infrastructure.Services
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        IEnumerable<Product> GetAll();
    }
}