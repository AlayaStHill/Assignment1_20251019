using Infrastructure;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class JsonFileRepository : IFileRepository
{
    private readonly string _filePath;

    public JsonFileRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveToFile()
    {
        //
        //skapa en relativ sökväg
    }

}
