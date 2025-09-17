using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private static List<ProductModel> _productList = new();

    public bool AddToProductList(ProductRequest productRequest)
    {
        if (!ValidateProductRequest.IsValid(productRequest))
                return false;

        if (!ValidateProductName.IsValid(productRequest.Name))
                return false;

        if (!ValidateProductPrice.IsValid(productRequest.Price))
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
