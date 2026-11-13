// шар між C#  і базою даних
using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    // Налаштування 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet = таблиця Movies, в якій будут Movie
    // тепер можемо писати: _db.Movies.Where(...) в сервісі
    public DbSet<Movie> Movies => Set<Movie>();
    
}