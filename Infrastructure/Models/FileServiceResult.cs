namespace Infrastructure.Models;

public class FileServiceResult
{
    public bool Succeeded { get; set; }
    public string? Error { get; set; }
    public string? Content { get; set; } // för testbarhet, json-sträng från filen (när används? när man vill returnera json i .Returns.();-delen i tester av getcontentfromfile) 
    
}

public class FileServiceResult<T> : FileServiceResult
{
    // Deserialiserad modell
    public T? Data { get; set; } 
}
