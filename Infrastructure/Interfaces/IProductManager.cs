using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IProductManager
{
    public bool SaveProduct(Product newProduct);

    IEnumerable<Product> GetAllProducts();
}
