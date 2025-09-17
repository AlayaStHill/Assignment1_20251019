using Infrastructure.Interfaces;
using System.Text.Json;

namespace Infrastructure.Services;

public class JsonFileRepository : IFileRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;

    public JsonFileRepository(string filePath, JsonSerializerOptions? options = null)
    {
        _filePath = filePath;
        _options = options ?? new JsonSerializerOptions(JsonSerializerDefaults.Web) 
        {
            WriteIndented = true
        };
    }

    public bool SaveContentToFile(string content) 
    {
        try
        {
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_filePath, content);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetContentFromFile()
    { 
        if (!File.Exists(_filePath))
        {
            return string.Empty;
        }
        string content = File.ReadAllText(_filePath);
        return content;

    }

    public bool SaveObjectAsJson<T>(T content)
    {
        try
        {
            string json = JsonSerializer.Serialize(content, _options);
            
            bool isSaved = SaveContentToFile(json);
            return isSaved;
        }
        catch
        {
            return false;
        }
    }
    public T? LoadObjectFromJson<T>()
    {
        try
        {
            string content = GetContentFromFile();
            if (string.IsNullOrWhiteSpace(content))
            {
                return default; // blir null om det misslyckas
            }
               

            T? result = JsonSerializer.Deserialize<T>(content, _options); 
            return result;
        }
        catch
        {
            return default;

        }
    }
}
