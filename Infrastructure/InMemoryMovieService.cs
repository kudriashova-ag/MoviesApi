using AutoMapper;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Infrastructure;

public class InMemoryMovieService : IMovieService
{
    private static readonly Lock Sync = new();

    private readonly IMapper _mapper;

    public InMemoryMovieService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    private static readonly List<Movie> Movies =
    [
        new() { Id = 1, Title = "Тіні забутих предків", ReleaseYear = 1965, Genre = "Драма", Rating = 8.0, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 2, Title = "Inception", ReleaseYear = 2010, Genre = "Фантастика", Rating = 8.8, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 3, Title = "The Matrix", ReleaseYear = 1999, Genre = "Фантастика", Rating = 8.7, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 4, Title = "Кіборги", ReleaseYear = 2017, Genre = "Воєнна драма", Rating = 8.0, CreatedAtUtc = DateTime.UtcNow },
        new() { Id = 5, Title = "Interstellar", ReleaseYear = 2014, Genre = "Фантастика", Rating = 8.7, CreatedAtUtc = DateTime.UtcNow }
    ];

    public IReadOnlyList<MovieDto> GetAll(string? genre)
    {
        if(!string.IsNullOrWhiteSpace(genre))
        {
            return Movies.Where(m => m.Genre.Equals(genre.Trim(), StringComparison.OrdinalIgnoreCase))
                .Select(m => _mapper.Map<MovieDto>(m)).ToList();
        }
        return Movies.Select(m => _mapper.Map<MovieDto>(m)).ToList();
    }


    public MovieDto? GetById(int id) {
        var movie = Movies.FirstOrDefault(m => m.Id == id);
        return movie is null ? null : _mapper.Map<MovieDto>(movie);
    }



    public MovieDto Create(CreateMovieDto dto)
    {
        lock (Sync) // використовувати, поки не  перейшли на БД
        {
            var movie = _mapper.Map<Movie>(dto);
            movie.Id = Movies.Count == 0 ? 1 : Movies.Max(m => m.Id) + 1;
            movie.CreatedAtUtc = DateTime.UtcNow;

            Movies.Add(movie);
            return _mapper.Map<MovieDto>(movie);
        }
    }

    public bool Delete(int id)
    {
        lock (Sync)
        {
            return Movies.RemoveAll(m => m.Id == id) > 0;
        }
    }



    public bool Update(int id, UpdateMovieDto dto)
    {
        lock (Sync)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie is null)
            {
                return false;
            }

            _mapper.Map(dto, movie);
            return true;
        }
    }

}