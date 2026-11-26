using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesApi.Domain.Movies;

namespace MoviesApi.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Genre)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.PosterPath)
            .HasMaxLength(200);

        builder.Property(m => m.ScriptPath)
            .HasMaxLength(200);


        builder.HasData(
           new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, Genre = "Sci-Fi" },
           new Movie { Id = 2, Title = "The Dark Knight", ReleaseYear = 2008, Genre = "Action" },
           new Movie { Id = 3, Title = "Interstellar", ReleaseYear = 2014, Genre = "Sci-Fi" },
           new Movie { Id = 4, Title = "Parasite", ReleaseYear = 2019, Genre = "Thriller" },
           new Movie { Id = 5, Title = "The Shawshank Redemption", ReleaseYear = 1994, Genre = "Drama" });
    }
} 