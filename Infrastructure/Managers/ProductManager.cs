using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Managers;

public class ProductManager(IProductService productService, IFileRepository fileRepository) : IProductManager
{
    private readonly IProductService _productService = productService;
    private readonly IFileRepository _fileRepository = fileRepository;

    public bool SaveProduct(ProductRequest productRequest)
    {
        bool isAdded = _productService.AddToProductList(productRequest);
        if (isAdded)
        {
            IEnumerable<ProductModel> productList = _productService.GetProductList();

            bool isSaved = _fileRepository.SaveObjectAsJson<IEnumerable<ProductModel>>(productList);
            return isSaved;
        }

        return false;
    }


    public IEnumerable<ProductResponse> GetAllProducts()
    {
        IEnumerable<ProductModel>? productListFromFile = _fileRepository.LoadObjectFromJson<IEnumerable<ProductModel>>();
        if (productListFromFile != null)
        {
            _productService.PopulateProductList(productListFromFile);
        }

        IEnumerable<ProductModel> productListFromMemory = _productService.GetProductList();

        IEnumerable<ProductResponse> responseList = productListFromMemory.Select(productModel => ProductFactory.MapModelToResponse(productModel));

        return responseList;

    }
}
