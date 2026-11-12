namespace MoviesApi.Application.Movies.Dtos;

//  Dto - форма даних для клієнта
public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string DisplayTitle { get; set; } = string.Empty;
}