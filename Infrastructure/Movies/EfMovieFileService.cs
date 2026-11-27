using MoviesApi.Application.Movies;
using MoviesApi.Infrastructure.Persistence;

namespace MoviesApi.Infrastructure.Movies;

public class EfMovieFileService : IMovieFileService
{
    private readonly AppDbContext _db;
    
}