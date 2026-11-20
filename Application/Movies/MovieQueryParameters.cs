using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Application.Movies;

public class MovieQueryParameters
{
    [Range(1, 10_000)]
    public int Page { get; set; } = 1;

    [Range(1, 50)]
    public int PageSize { get; set; } = 2;

}