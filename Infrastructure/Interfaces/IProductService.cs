using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        bool AddToProductList(ProductRequest productRequest);
        IEnumerable<ProductModel> GetProductList();

        void PopulateProductList(IEnumerable<ProductModel> productListFromFile);
    }
}