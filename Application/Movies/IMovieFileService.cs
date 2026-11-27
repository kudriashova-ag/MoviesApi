// Шар Application: що програма вміє робити з файлами фільму.
// Окремий інтерфейс, а не нові методи в IMovieService: CRUD фільмів і файли — різні задачі,
// і InMemoryMovieService не мусить уміти зберігати файли.
using MoviesApi.Application.Files;
using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Application.Movies;

public interface IMovieFileService
{
    /// <summary>Зберігає (або замінює) постер. null — фільму немає.</summary>
    Task<MovieDto?> SetPosterAsync(int movieId, UploadedFile file);

    /// <summary>Видаляє постер. false — фільму немає.</summary>
    Task<bool> DeletePosterAsync(int movieId);

    /// <summary>Зберігає (або замінює) сценарій. null — фільму немає.</summary>
    Task<MovieDto?> SetScriptAsync(int movieId, UploadedFile file);

    /// <summary>Чи є у фільму сценарій.</summary>
    Task<bool> HasScriptAsync(int movieId);

    /// <summary>Сценарій для скачування. null — фільму або сценарію немає.</summary>
    // Task<DownloadFile?> GetScriptAsync(int movieId);
}