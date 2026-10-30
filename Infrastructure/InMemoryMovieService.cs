using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Infrastructure;

public class InMemoryMovieService : IMovieService
{

    private static readonly List<Movie> Movies =
    [
        new() { Id = 1, Title = "Тіні забутих предків", ReleaseYear = 1965, Genre = "Драма", Rating = 8.0, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 2, Title = "Inception", ReleaseYear = 2010, Genre = "Фантастика", Rating = 8.8, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 3, Title = "The Matrix", ReleaseYear = 1999, Genre = "Фантастика", Rating = 8.7, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 4, Title = "Кіборги", ReleaseYear = 2017, Genre = "Воєнна драма", Rating = 8.0, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 5, Title = "Interstellar", ReleaseYear = 2014, Genre = "Фантастика", Rating = 8.7, CreatedAtUtc = DateTime.UtcNow }
    ];

    public IReadOnlyList<MovieDto> GetAll() => Movies.Select(m => m.ToDto()).ToList();
    
    public MovieDto? GetById(int id) => Movies.FirstOrDefault(m => m.Id == id)?.ToDto();
}