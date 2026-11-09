using Microsoft.AspNetCore.Mvc;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "v2")]
public class MoviesV2Controller : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesV2Controller(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Повертає всі фільми (версія 2)
    /// </summary>
    /// <param name="genre"> Жанр фільму </param>
    /// <returns> Масив фільмів </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MovieDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<MovieDto>> GetAll([FromQuery] string? genre)
    {
        var movies = _movieService.GetAll(genre);
        return Ok(movies);
    }

}
