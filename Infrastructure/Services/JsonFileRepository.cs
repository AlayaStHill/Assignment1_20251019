using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class JsonFileRepository : IFileRepository
{
    public bool SaveContentToFile(string filePath, string content) 
    {
        try
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(filePath, content);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public string GetContentFromFile(string filePath)
    { 
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        return File.ReadAllText(filePath);

    }

}
