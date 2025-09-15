using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Managers;

public class ProductManager(IProductService productService, IFileRepository fileRepository) : IProductManager
{
    private readonly IProductService _productService = productService;
    private readonly IFileRepository _fileRepository = fileRepository;

    public bool SaveProduct(ProductRequest productRequest)
    {
        bool addSuccess = _productService.AddToProductList(productRequest);
        if (addSuccess)
        {
            IEnumerable<ProductModel> productList = _productService.GetProductList();

            bool saveResult = _fileRepository.SaveObjectAsJson<IEnumerable<ProductModel>>(productList);
            return saveResult;
        }

        return false;
    }

    public IEnumerable<ProductModel> GetAllProducts()
    {
        IEnumerable<ProductModel> productList = _productService.GetProductList();
        return productList;
    }
}
