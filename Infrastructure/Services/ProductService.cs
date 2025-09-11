using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService(IFileRepository fileRepository) : IProductService
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private static List<Product> _productList = new();

    public void CreateProduct(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        _productList.Add(product);

        //_fileRepository.SaveToFile();
    }

    public IEnumerable<Product> GetAll()
    {
        return _productList;
    }
}
