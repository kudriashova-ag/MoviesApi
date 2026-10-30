using Microsoft.AspNetCore.Mvc;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }


    [HttpGet]
    public ActionResult<IReadOnlyList<MovieDto>> GetAll()
    {
        var movies = _movieService.GetAll();
        return Ok(movies);
    }
}