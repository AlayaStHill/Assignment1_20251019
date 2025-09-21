using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Moq;
using System.Text.Json;

namespace Infrastructure.Tests.Services;
/* 
FileService = SaveObjectAsJson<T> (_jsonFileRepository.SaveContentToFile = true) + LoadObjectFromJson<T> (_jsonFileRepository.GetContentFromFile = json-content)
Mock<IFileRepository>: 
- .Object är en property i Mock<T>-klassen. .Object implementerar IFileRepository = agerar en låtsas-JsonFileRepository
- hur .Object ska agera i testet styrs av Mock<IFileRepository>-instansen _jsonFileRepoMock, via metoder som .Setup(...), .Returns(...), .Verify(...)...
*/
public class FileService_Tests
{

    private readonly Mock<IFileRepository> _jsonFileRepoMock;
    private readonly JsonSerializerOptions _options;
    private readonly FileService _fileService;

    public FileService_Tests()
    {
        _jsonFileRepoMock = new Mock<IFileRepository>();

        _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            // JsonSerializerOptions(JsonSerializerDefaults.Web) = escapar åäö bl.a. äpple --> \u00e4pple i json-text. 
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

        };
        _fileService = new FileService(_jsonFileRepoMock.Object, "fakeFilePath", _options);
    }

    // Happy-path
    [Fact]
    public void SaveObjectAsJson_ShouldSucceed_WhenRepositoryReturnsTrue()
    {
        // ARRANGE - tar in en IEnumerable skickad från productManager 
        // När SaveObjectAsJson anropar SaveContentToFile och skickar med _filePath och "vilken sträng som helst", ska mocken returnera true
        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        IEnumerable<ProductModel> productList = new List<ProductModel>
        {
            new ProductModel { ProductName = "banan", Price = 10m },
            new ProductModel { ProductName = "äpple", Price = 5m }
        };

        // ACT
        FileServiceResult result = _fileService.SaveObjectAsJson(productList);


        // ASSERT
        Assert.True(result.Succeeded);
        // Ska finnas en json-sträng
        Assert.NotNull(result.Content);
        Assert.Contains("äpple", result.Content!);
        Assert.Contains("banan", result.Content!);
        // Mockens egna assertion. Kontrollera att SaveContentToFile anropades endast en gång med _filePath och "vilken sträng som helst"
        _jsonFileRepoMock.Verify(jsonFileRepoMock => jsonFileRepoMock.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

    }

    [Fact]
    public void SaveObjectAsJson_ShouldReturnFalseWithError_WhenRepositoryReturnsFalse()
    {
        // ARRANGE 
        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        IEnumerable<ProductModel> productList = new List<ProductModel>
        {
            new ProductModel { ProductName = "banan", Price = 10m },
        };

        // ACT
        FileServiceResult result = _fileService.SaveObjectAsJson(productList);


        // ASSERT
        Assert.False(result.Succeeded);
        Assert.Equal("Kunde inte spara till fil", result.Error);
        _jsonFileRepoMock.Verify(jsonFileRepoMock => jsonFileRepoMock.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

    }

    // Simulerar ett oförutsett fel (exception) vid filhantering
    [Fact]
    public void SaveObjectAsJson_ShouldReturnFalseWithError_WhenExceptionOccured()
    {
        // ARRANGE 
        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.SaveContentToFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new IOException("ex.Message"));

        IEnumerable<ProductModel> productList = new List<ProductModel>
        {
            new ProductModel { ProductName = "banan", Price = 10m },
        };

        // ACT
        FileServiceResult result = _fileService.SaveObjectAsJson(productList);


        // ASSERT - kollar att FileService stoppat något felmeddelande i Error vid krasch
        Assert.False(result.Succeeded);
        Assert.False(string.IsNullOrEmpty(result.Error));
    }

    // Happy path test
    [Fact]
    public void LoadObjectAsJson_ShouldSucceed_WhenRepositoryReturnsValidJson()
    {
        // ARRANGE 
        string json = "[{\"ProductName\":\"banan\",\"Price\":10},{\"ProductName\":\"äpple\",\"Price\":5}]";

        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()))
            .Returns(json);


        // ACT
        FileServiceResult<IEnumerable<ProductModel>> result = _fileService.LoadObjectFromJson<IEnumerable<ProductModel>>();


        // ASSERT
        Assert.True(result.Succeeded);
        // Skyddar mot NullReferenceException i nästa asserts
        Assert.NotNull(result.Data);
        // LINQ
        Assert.Equal(2, result.Data!.Count());
        Assert.Contains(result.Data!, productModel => productModel.ProductName == "banan");
        Assert.Contains(result.Data!, productModel => productModel.ProductName == "äpple");
        Assert.Null(result.Error);

    }

    //Negative case test
    [Fact]
    public void LoadObjectAsJson_ShouldReturnFalseWithErrorAndEmptyString_WhenFileNotFound()
    {
        // ARRANGE 

        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()))
            .Returns(string.Empty);


        // ACT
        FileServiceResult<IEnumerable<ProductModel>> result = _fileService.LoadObjectFromJson<IEnumerable<ProductModel>>();


        // ASSERT
        Assert.False(result.Succeeded);
        Assert.Null(result.Data);
        Assert.NotNull(result.Content);
        Assert.Empty(result.Content);
        Assert.Equal("Filen kunde inte hittas eller var tom.", result.Error);
        _jsonFileRepoMock.Verify(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void LoadObjectFromJson_ShouldReturnFalseWithError_WhenJsonIsInvalid()
    {
        // ARRANGE 
        string invalidJson = "Invalid json";
        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()))
            .Returns(invalidJson);

        // ACT
        FileServiceResult<IEnumerable<ProductModel>> result = _fileService.LoadObjectFromJson<IEnumerable<ProductModel>>();

        // ASSERT
        Assert.False(result.Succeeded);
        Assert.Null(result.Data);
        Assert.False(string.IsNullOrEmpty(result.Error)); 
        _jsonFileRepoMock.Verify(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void LoadObjectFromJson_ShouldReturnFalseWithError_WhenExceptionOccured()
    {
        // ARRANGE 
        _jsonFileRepoMock
            .Setup(jsonFileRepoMock => jsonFileRepoMock.GetContentFromFile(It.IsAny<string>()))
            .Throws(new IOException("ex.Message"));

        IEnumerable<ProductModel> productList = new List<ProductModel>
        {
            new ProductModel { ProductName = "banan", Price = 10m },
        };

        // ACT
        FileServiceResult<IEnumerable<ProductModel>> result = _fileService.LoadObjectFromJson<IEnumerable<ProductModel>>();


        // ASSERT 
        Assert.False(result.Succeeded);
        Assert.False(string.IsNullOrEmpty(result.Error));
    }
    
}





