using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private static List<Product> _productList = new();

    public void CreateProduct(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        _productList.Add(product);
    }

    public IEnumerable<Product> GetAll()
    {
        return _productList;
    }
}
