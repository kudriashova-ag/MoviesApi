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


    [HttpGet] // GET  api/movies?genre=Фантастика
    public ActionResult<IReadOnlyList<MovieDto>> GetAll([FromQuery] string? genre)
    {
        var movies = _movieService.GetAll(genre);
        return Ok(movies);
    }


    [HttpGet("{id:int:min(1)}")]  // GET  api/movies/1
    public ActionResult<MovieDto> GetById([FromRoute] int id)
    {
        var movie = _movieService.GetById(id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie);
    }


    [HttpPost]  // POST  api/movies
    public ActionResult Create([FromBody] CreateMovieDto dto)
    {
        return Ok("OK");
    }
    
}