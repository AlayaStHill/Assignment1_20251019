namespace Infrastructure.Interfaces
{
    public interface IFileRepository 
    {
        bool SaveContentToFile(string filePath, string content);
        string GetContentFromFile(string filePath);

    }
}