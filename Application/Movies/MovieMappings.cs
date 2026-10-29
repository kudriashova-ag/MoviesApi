using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Application.Movies;
// Mapping - копіювання властивостей одного об'єкту в інший

public static class MovieMappings
{
    public static MovieDto ToDto(this Movie movie) =>
        new MovieDto(movie.Id, movie.Title, movie.ReleaseYear, movie.Genre, movie.Rating);

}
