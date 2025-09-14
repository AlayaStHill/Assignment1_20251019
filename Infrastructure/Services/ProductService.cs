using Infrastructure.Interfaces;
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

        //_fileRepository.SaveToFile(JsonSerializer.Serialize(_productList);
    }
    /*
    Använd ingen Console.WriteLine i ProductService
    Gör nullchecken i UIService men såhär kan man göra också:

  public string CreateProduct(Product product)
    {
        if (product == null)
        {
            return "Invalid object provided"; etc...
        }

        var json = _fileRepository.ConvertToJson(product);
        product.Id = Guid.NewGuid().ToString();
        _productList.Add(product);
    
    Man kan behöva göra om till rätt format. Antingen låter man fileService ta hand om formatändringen eller productService
        _fileRepository.SaveToFile(json); 
    */

    public IEnumerable<Product> GetAllProducts()
    {
        return _productList;
    }


}
