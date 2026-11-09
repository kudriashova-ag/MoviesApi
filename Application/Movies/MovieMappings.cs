using Microsoft.AspNetCore.Components.Web;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Application.Movies;
// Mapping - копіювання властивостей одного об'єкту в інший

public static class MovieMappings
{
    // Movie -> MovieDto
    public static MovieDto ToDto(this Movie movie) =>
        new MovieDto(movie.Id, movie.Title, movie.ReleaseYear, movie.Genre, movie.Rating);

    // CreateMovieDto -> нова сутність
    public static Movie ToEntity(this CreateMovieDto dto) => new()
    {
        Title = dto.Title.Trim(),
        ReleaseYear = dto.ReleaseYear,
        Genre = dto.Genre.Trim(),
        Rating = dto.Rating
    };

    public static void ApplyTo(this UpdateMovieDto dto, Movie movie)
    {
        movie.Title = dto.Title.Trim();
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Genre = dto.Genre.Trim();
        movie.Rating = dto.Rating;
    }

}
