namespace MoviesApi.Domain.Movies;

//  Сутність - з чим працює програма
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}