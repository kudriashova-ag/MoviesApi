using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;
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


}