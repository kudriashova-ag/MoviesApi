namespace MoviesApi.Application.Files;

public interface IFileStorageService
{
    Task<string> SaveAsync(Stream content, string folder, string extension, StorageZone zone);
    Task<Stream?> OpenReadAsync(string path, StorageZone zone);
    Task DeleteAsync(string path, StorageZone zone);
}