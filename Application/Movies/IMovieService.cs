using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Application.Movies;

//  Описує ЩО вміє робити програма з фільмами, але не ЯК
public interface IMovieService
{
    IReadOnlyList<MovieDto> GetAll(string? genre);

    MovieDto? GetById(int id);

    MovieDto Create(CreateMovieDto dto);

    bool Delete(int id);
}