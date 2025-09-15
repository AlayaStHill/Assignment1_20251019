using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService(IFileRepository fileRepository) : IProductService
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private static List<ProductModel> _productList = new();

    public bool AddToProductList(ProductRequest productRequest)
    {
        if (productRequest == null)
            return false;

        if (string.IsNullOrWhiteSpace(productRequest.Name))
            return false;

        if (productRequest.Price <= 0)
            return false;

        ProductModel newProduct = ProductFactory.MapRequestToModel(productRequest);

        _productList.Add(newProduct);

        return true;
    }

    public IEnumerable<ProductModel> GetProductList()
    {
        IEnumerable<ProductModel>? productList = _fileRepository.LoadObjectFromJson<IEnumerable<ProductModel>>();  
        if (productList == null)
        {
            return new List<ProductModel>(); //returnera null ?? hantera i? Annars retu
        }

        return productList;
    }




    public ProductResponse? GetProductByName(string Name) // göra metod i menuservice, hantera null-värden
    {
        IEnumerable<ProductModel>? productList = _fileRepository.LoadObjectFromJson<IEnumerable<ProductModel>>();
        if (productList == null)
        {
            return null;
        }

        ProductModel? product = productList.FirstOrDefault(product => product.Name == Name);
        if (product == null)
        {
            return null;
        }

        ProductResponse productResponse = ProductFactory.MapModelToResponse(product);

        return productResponse;

    }

    //public Product DeleteProduct(Product product)
    //{
    //    throw new NotImplementedException();
    //}

    //public Product UpdateProduct(Product product)
    //{
    //    throw new NotImplementedException();
    //}
}   
