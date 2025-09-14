namespace Infrastructure.Interfaces
{
    public interface IFileRepository
    {
        bool SaveObjectAsJson<T>(T content);
        T? LoadObjectFromJson<T>();
    }
}