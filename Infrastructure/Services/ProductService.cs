using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private static List<ProductModel> _productList = new();

    public bool AddToProductList(ProductRequest productRequest)
    {
        if (productRequest == null)
            return false;

        if (string.IsNullOrWhiteSpace(productRequest.Name))
            return false;

        if (productRequest.Price <= 0)
            return false;

        ProductModel newProduct = ProductFactory.MapRequestToModel(productRequest);

        _productList.Add(newProduct);

        return true;
    }

    public IEnumerable<ProductModel> GetProductList()
    {

        return _productList;
    }

    public void PopulateProductList(IEnumerable<ProductModel> productListFromFile)
    {
        _productList = productListFromFile.ToList();
    }
}   
