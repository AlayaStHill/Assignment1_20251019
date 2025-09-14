using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Managers;

public class ProductManager(IProductService productService, IFileRepository fileRepository) : IProductManager
{
    private readonly IProductService _productService = productService;
    private readonly IFileRepository _fileRepository = fileRepository;

    public bool SaveProduct(Product newProduct)
    {
        bool addSuccess = _productService.AddToProductList(newProduct);
        if (addSuccess)
        {
            IEnumerable<Product> productList = _productService.GetProductList();

            bool saveResult = _fileRepository.SaveObjectAsJson<IEnumerable<Product>>(productList);
            return saveResult;
        }

        return false;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        IEnumerable<Product> productList = _productService.GetProductList();
        return productList;
    }
}
