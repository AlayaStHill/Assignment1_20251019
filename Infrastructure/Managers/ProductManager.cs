using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Managers;

public class ProductManager(IProductService productService, IFileService fileService) : IProductManager
{
    private readonly IProductService _productService = productService;
    private readonly IFileService _fileService = fileService;

    public ProductServiceResult SaveProduct(ProductRequest productRequest)
    {
        ProductServiceResult addResult = _productService.AddToProductList(productRequest);
        if (addResult.Succeeded)
        {
            IEnumerable<ProductModel> productListFromMemory = _productService.GetProductList();

            FileServiceResult savedResult = _fileService.SaveObjectAsJson<IEnumerable<ProductModel>>(productListFromMemory); 

            if (savedResult.Succeeded)
            {
                return new ProductServiceResult
                {
                    Succeeded = true
                };
            }

            return new ProductServiceResult
            {
                Error = savedResult.Error 
            };
        }

        return addResult;
    }


    public IEnumerable<ProductResponse> GetAllProducts()
    {
        // Skickar in: Succeeded, Error, Content och Data
        FileServiceResult<IEnumerable<ProductModel>> isLoaded = _fileService.LoadObjectFromJson<IEnumerable<ProductModel>>();
        
        if (isLoaded.Succeeded)
        {
            // Data kan vara null: filen är tom, json ej deserialiserad, catchen. Men då är Succeeded = false. ! eftersom då: Succeeded är true = data --> result
            _productService.PopulateProductList(isLoaded.Data!); 
        }

        IEnumerable<ProductModel> productListFromMemory = _productService.GetProductList();

        return ProductFactory.MapModelsToResponse(productListFromMemory);

    }
}



    