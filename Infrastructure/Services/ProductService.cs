using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly List<ProductModel> _productList = new();

    public ProductServiceResult AddToProductList(ProductRequest productRequest)
    {
        if (!ProductRequestValidator.IsValid(productRequest))
            return new ProductServiceResult
            {
                Succeeded = false,
                Error = "ProductRequest är null"
            };

        if (!ProductNameValidator.IsValid(productRequest.ProductName))
            return new ProductServiceResult
            {
                Succeeded = false,
                Error = "Name är null eller tomt"
            };

        if (!ProductPriceValidator.IsValid(productRequest.Price))
            return new ProductServiceResult
            {
                Succeeded = false,
                Error = "Price är 0 eller negativt"
            };

        ProductModel newProduct = ProductFactory.MapRequestToModel(productRequest);

        _productList.Add(newProduct);

        return new ProductServiceResult { Succeeded = true }; 
    }

    public IEnumerable<ProductModel> GetProductList()
    {

        return _productList;
    }

    public void PopulateProductList(IEnumerable<ProductModel> productListFromFile)
    {
        _productList.Clear();
        _productList.AddRange(productListFromFile);
    } 
}   
