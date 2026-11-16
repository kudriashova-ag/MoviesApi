using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Application.Movies;

//  Описує ЩО вміє робити програма з фільмами, але не ЯК
public interface IMovieService
{
    Task<IReadOnlyList<MovieDto>> GetAllAsync(string? genre);

    Task<MovieDto?> GetByIdAsync(int id);

    Task<MovieDto> CreateAsync(CreateMovieDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateAsync(int id, UpdateMovieDto dto);
}