using Infrastructure;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class JsonFileService : IFileService
{
    private readonly string _filePath;

    public JsonFileService(string filePath)
    {
        _filePath = filePath;
    }

   
}
