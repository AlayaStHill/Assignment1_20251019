using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class ProductFactory
{
    public static ProductResponse MapModelToResponse(ProductModel product)
    {
        return new ProductResponse
        {
            ProductName = product.ProductName, // Måste jag ha Id med här?
            Description = product.Description,
            Price = product.Price,
        };
    }

    public static IEnumerable<ProductResponse> MapModelsToResponse(IEnumerable<ProductModel> productModelsList) 
    {
        IEnumerable<ProductResponse> productResponseList = productModelsList.Select(productModel => MapModelToResponse(productModel));
        return productResponseList;
    }

    public static ProductModel MapRequestToModel(ProductRequest productRequest)
    {
        return new ProductModel
        {
            Id = IdGenerator.GenerateId(),
            ProductName = productRequest.ProductName,
            Description = productRequest.Description,
            Price = productRequest.Price
        };
    }

}

