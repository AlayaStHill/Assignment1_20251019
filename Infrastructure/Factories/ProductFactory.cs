using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class ProductFactory
{
    public static ProductResponse MapModelToResponse(ProductModel product)
    {
        return new ProductResponse
        {
            Name = product.Name,
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
            Name = productRequest.Name,
            Description = productRequest.Description,
            Price = productRequest.Price
        };
    }

}

