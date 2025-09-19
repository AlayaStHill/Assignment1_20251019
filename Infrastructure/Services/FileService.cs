using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Text.Json;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IFileRepository _jsonFileRepository;
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;

    public FileService(IFileRepository jsonFileRepository, string filePath, JsonSerializerOptions? options = null)
    {
        _jsonFileRepository = jsonFileRepository;
        _filePath = filePath;
        _options = options ?? new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };
    }

    public FileServiceResult SaveObjectAsJson<T>(T content)
    {
        try
        {
            string json = JsonSerializer.Serialize(content, _options);
            
            bool isSaved = _jsonFileRepository.SaveContentToFile(_filePath, json);
            return new FileServiceResult
            {
                // Eller if/else
                Succeeded = isSaved,
                //Ternary operator: condition ? valueReturnedIfTrue : valueReturnedIfFalse; 
                Error = isSaved ? null : "Kunde inte spara till fil",
                Content = json // testbarhet
            };
        }
        catch (Exception ex)
        {
            return new FileServiceResult
            {
                Succeeded = false,
                //Message = Exception-egenskap
                Error = ex.Message
            };
        }
    }

 
    public FileServiceResult<T> LoadObjectFromJson<T>()
    {
        try
        {
            string content = _jsonFileRepository.GetContentFromFile(_filePath);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new FileServiceResult<T>
                {
                    Succeeded = false,
                    Error = "Filen kunde inte hittas eller var tom.",
                    Content = content // testbarhet
                };
            }

            T? result = JsonSerializer.Deserialize<T>(content, _options);

            return new FileServiceResult<T> 
            {
                Succeeded = result != null,
                Error = result == null ? "Deserialisering misslyckades." : null,
                Content = content, // testbarhet
                Data = result
            };

            /*
              ISTÄLLET FÖR:
              if (result != null)
            {
                return new JsonFileRepoResult
                {
                    Succeeded = true,
                    Content = content,
                    Error = null
                };
            }
            */
        }
        catch (Exception ex) 
        {
            return new FileServiceResult<T>
            {
                Succeeded = false,
                Error = ex.Message
            };

        }
    }
}