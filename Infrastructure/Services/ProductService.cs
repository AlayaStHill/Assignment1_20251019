using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ProductService(IFileRepository fileRepository) : IProductService
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private static List<Product> _productList = new();

    public bool AddToProductList(Product newProduct)
    {
        if (newProduct == null)
            return false;

        if (string.IsNullOrWhiteSpace(newProduct.Name))
            return false;

        if (newProduct.Price <= 0)
            return false;

        newProduct.Id = Guid.NewGuid().ToString();
        _productList.Add(newProduct);

        return true;
    }

    public IEnumerable<Product> GetProductList()
    {
        return _productList;
    }




    //public Product GetProductById(string id)
    //{
    //    throw new NotImplementedException();
    //    //Lambda first or default. ANvända frågetecken, kan vara tomma?
    //}

    //public Product GetProductByName(string name)
    //{
    //    throw new NotImplementedException();
    //    //Lambda first or default. ANvända frågetecken, kan vara tomma?

    //}

    //public Product DeleteProduct(Product product)
    //{
    //    throw new NotImplementedException();
    //}

    //public Product UpdateProduct(Product product)
    //{
    //    throw new NotImplementedException();
    //}
}
