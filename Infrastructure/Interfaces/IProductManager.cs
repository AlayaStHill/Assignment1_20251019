using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IProductManager
{
    public bool SaveProduct(ProductRequest productRequest);

    IEnumerable<ProductResponse> GetAllProducts();
}
