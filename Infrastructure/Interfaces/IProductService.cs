using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        bool AddToProductList(Product newProduct);
        IEnumerable<Product> GetProductList();

        //Product GetProductById(string id);

        //Product GetProductByName(string name);

        //Product DeleteProduct(Product product);

        //Product UpdateProduct(Product product);

    }
}