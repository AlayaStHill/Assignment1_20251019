using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IFileService
{
    FileServiceResult SaveObjectAsJson<T>(T content);
    FileServiceResult<T> LoadObjectFromJson<T>();
}
