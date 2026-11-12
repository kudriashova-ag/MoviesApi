using AutoMapper;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Application.Movies;

/// <summary>
/// Усі правила маппінгу (перетворення одного об'єкту в інший)
/// </summary>
public class MovieProfile : Profile
{
    public MovieProfile()
    {
        // Movie -> MovieDto  
        // поля з однаковими назвами копіює сам
        CreateMap<Movie, MovieDto>()
            .ForMember(d => d.DisplayTitle, o => o.MapFrom(m => $"{m.Title} ({m.ReleaseYear})"));

        // CreateMovieDto -> Movie
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedAtUtc, o => o.Ignore());
        
        // UpdateMovieDto -> Movie
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedAtUtc, o => o.Ignore());
    }
}