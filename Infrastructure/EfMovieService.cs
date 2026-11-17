using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;
using MoviesApi.Domain.Movies;
using MoviesApi.Infrastructure.Persistence;

namespace MoviesApi.Infrastructure;


public class EfMovieService : IMovieService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EfMovieService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<MovieDto>> GetAllAsync(string? genre)
    {
        return await _db.Movies
            .OrderBy(m => m.Id)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        return await _db.Movies
            .Where(m => m.Id == id)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<MovieDto> CreateAsync(CreateMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _db.Movies.FindAsync(id);
        if (movie is null) return false;
        _db.Movies.Remove(movie);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(int id, UpdateMovieDto dto)
    {
        var movie = await _db.Movies.FindAsync(id);
        if (movie is null) return false;

        _mapper.Map(dto, movie); // копіюємо поля з dto в movie
        await _db.SaveChangesAsync();
        return true;
    }
}