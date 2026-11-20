using AutoMapper;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Infrastructure;

public class InMemoryMovieService 
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

    public Task<IReadOnlyList<MovieDto>> GetAllAsync(string? genre)
    {
        if (!string.IsNullOrWhiteSpace(genre))
        {
            IReadOnlyList<MovieDto> result1 = Movies.Where(m => m.Genre.Equals(genre.Trim(), StringComparison.OrdinalIgnoreCase))
                .Select(m => _mapper.Map<MovieDto>(m)).ToList();
            return Task.FromResult(result1);
        }
        IReadOnlyList<MovieDto> result = Movies.Select(m => _mapper.Map<MovieDto>(m)).ToList();
        return Task.FromResult(result);
    }


    public Task<MovieDto?> GetByIdAsync(int id) {
        var movie = Movies.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(movie is null ? null : _mapper.Map<MovieDto>(movie));
    }



    public Task<MovieDto> CreateAsync(CreateMovieDto dto)
    {
        lock (Sync) // використовувати, поки не  перейшли на БД
        {
            var movie = _mapper.Map<Movie>(dto);
            movie.Id = Movies.Count == 0 ? 1 : Movies.Max(m => m.Id) + 1;
            movie.CreatedAtUtc = DateTime.UtcNow;

            Movies.Add(movie);
            return Task.FromResult(_mapper.Map<MovieDto>(movie));
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (Sync)
        {
            return Task.FromResult(Movies.RemoveAll(m => m.Id == id) > 0);
        }
    }



    public Task<bool> UpdateAsync(int id, UpdateMovieDto dto)
    {
        lock (Sync)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie is null)
            {
                return Task.FromResult(false);
            }

            _mapper.Map(dto, movie);
            return Task.FromResult(true);
        }
    }

}