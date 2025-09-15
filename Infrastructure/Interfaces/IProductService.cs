using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        bool AddToProductList(ProductRequest productRequest);
        IEnumerable<ProductModel> GetProductList();

        //Product GetProductById(string id);

        //Product GetProductByName(string name);

        //Product DeleteProduct(Product product);

        //Product UpdateProduct(Product product);

    }
}