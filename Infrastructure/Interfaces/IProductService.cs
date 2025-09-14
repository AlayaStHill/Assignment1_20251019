using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        bool AddToList(Product product);
        IEnumerable<Product> GetAllProducts();

    }
}