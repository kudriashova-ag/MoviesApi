using MoviesApi.Application.Files;

namespace MoviesApi.Infrastructure.Files;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _publicRoot;
    private readonly string _privateRoot;

    public LocalFileStorageService(string publicRoot, string privateRoot)
    {
        _publicRoot = publicRoot;
        _privateRoot = privateRoot;
    }

    // Перетворює "posters/abc.jpg" на повний шлях і перевіряє, що він НЕ виходить за межі зони.
    // Другий рубіж захисту (defense in depth): навіть якщо колись шлях прийде «ззовні»
    // і в ньому буде "../", файл поза папкою сховища ми не відкриємо і не видалимо.
    private string ResolveFullPath(string path, StorageZone zone)
    {
        var root = zone == StorageZone.Public ? _publicRoot : _privateRoot;
        var fullPath = Path.GetFullPath(Path.Combine(root, path));

        if (!fullPath.StartsWith(root + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            throw new InvalidOperationException($"Шлях '{path}' виходить за межі сховища.");

        return fullPath;
    }

    // banner_main/cars.jpg, "wwwroot"
    public Task DeleteAsync(string path, StorageZone zone)
    {
        var fullPath = ResolveFullPath(path, zone);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        return Task.CompletedTask;
    }

    public Task<Stream?> OpenReadAsync(string path, StorageZone zone)
    {
        var fullPath = ResolveFullPath(path, zone);
        Stream? stream = File.Exists(fullPath) ? File.OpenRead(fullPath) : null;
        return Task.FromResult(stream);
    }

    
    public async Task<string> SaveAsync(Stream content, string folder, string extension, StorageZone zone)
    {
        var path = $"{folder}/{Guid.NewGuid():N}{extension}";
        var fullPath = ResolveFullPath(path, zone);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using var target = new FileStream(fullPath, FileMode.CreateNew);
        await content.CopyToAsync(target);

        return path;
    }



}