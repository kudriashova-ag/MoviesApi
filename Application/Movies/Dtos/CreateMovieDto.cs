using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Application.Movies.Dtos;

// DTO для створення нового фільму (те, що клієнт надсилає в тілі POST-запиту)
public class CreateMovieDto
{
    [Required(ErrorMessage = "Назва фільму обов'язкова")]
    [StringLength(200, ErrorMessage = "Назва < 200 символів")]
    public string Title { get; set; } = null!;

    [Range(1888, 2026)]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Жанр фільму обов'язковий")]
    [StringLength(50, ErrorMessage = "Жанр < 50 символів")]
    public string Genre { get; set; } = null!;

    [Range(0.0, 10.0)]
    public double Rating { get; set; }
}