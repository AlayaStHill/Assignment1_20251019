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
            Price = product.Price
        };
    }

    public static ProductModel MapRequestToModel(ProductRequest productRequest)
    {
        return new ProductModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = productRequest.Name,
            Description = productRequest.Description,
            Price = productRequest.Price
        };
    }

}
