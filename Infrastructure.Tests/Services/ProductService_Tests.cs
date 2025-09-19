using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Tests.Services;
// Edge case?
public class ProductService_Tests
{
    private readonly ProductService _productService;

    public ProductService_Tests()
    {
        _productService = new ProductService(); // ny instans för varje test
    }

    // Happy-path test 
    [Fact]
    public void AddProductToList_ShouldSucceed_WhenProductIsAddedToList()
    {
        // ARRANGE - konfigurering, behöver tillgång till en produkt för att kunna utföra testet, productservice via konstruktorn
       
        ProductRequest product = new ProductRequest 
        { 
            Id = "1", 
            ProductName = "banan", 
            Description = "gul", 
            Price = 10m 
        };


        // ACT - försöker lägga till produkten i listan. ProductServiceResult istället för bool, ger utförligare svar på vad som kan ha gått fel om testet misslyckas
        ProductServiceResult result = _productService.AddToProductList(product);


        // ASSERT - kontroller av förväntat resultat. Flera olika kontroller för att säkerställa önskat resultat
        // True - kontrollerar metodens respons. True om metoden returnerar att den lyckats, garanterar inte nödvändigtvis att produkten faktiskt har lagts till i listan.
        Assert.True(result.Succeeded);
        //Single - Kontrollerar listans count. Lyckas om listans count == 1. 
        Assert.Single(_productService.GetProductList());
        // Equal - kontrollerar listans innehåll, (expected, actual). Om ett Assert misslyckas, avbryts testet och resten av koden i testet körs inte. Kan därför använda !
        Assert.Equal(product.ProductName, _productService.GetProductList().FirstOrDefault()!.ProductName);
    }

    // Negative test case 
    [Fact]
    public void AddProductToList_ShouldReturnError_WhenProductRequestIsNull()
    {
        // ARRANGE
        ProductRequest? product = null;

        // ACT
        ProductServiceResult addResult = _productService.AddToProductList(product!);

        // ASSERT
        Assert.False(addResult.Succeeded);
        Assert.Equal("ProductRequest är null", addResult.Error);
        Assert.Empty(_productService.GetProductList());
    }


    // Negative test case
    [Theory]
    [InlineData("", 10, "Name är null eller tomt")]
    [InlineData("  ", 10, "Name är null eller tomt")]
    [InlineData("banan", 0, "Price är 0 eller negativt")]  
    [InlineData("äpple", -10, "Price är 0 eller negativt")] 
    public void AddProductToList_ShouldReturnError_WhenProductIsInvalid(string productName, decimal productPrice, string expectedError)
    {
        // ARRANGE
        var product = new ProductRequest
        {
            ProductName = productName,
            Price = productPrice
        };


        // ACT
        ProductServiceResult addResult = _productService.AddToProductList(product);


        // ASSERT
        Assert.False(addResult.Succeeded);
        // Förväntar mig att felmeddelandet alltid innehåller något
        Assert.NotNull(addResult.Error);
        // Ska vara tom när inmatningarna är fel
        Assert.Empty(_productService.GetProductList());
        Assert.Equal(expectedError, addResult.Error);
    }
}

