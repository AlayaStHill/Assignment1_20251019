using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IProductManager
{
    public ProductServiceResult SaveProduct(ProductRequest productRequest);

    IEnumerable<ProductResponse> GetAllProducts();
}
