using Microsoft.AspNetCore.Mvc;
using MoviesApi.Application.Movies;
using MoviesApi.Application.Movies.Dtos;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Повертає всі фільми
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

    /// <summary>
    /// Повертає фільм за id
    /// </summary>
    /// <param name="id">Ідентифікатор фільму</param>
    /// <returns>Фільм або 404, якщо його нема</returns>
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieDto> GetById([FromRoute] int id)
    {
        var movie = _movieService.GetById(id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    /// <summary>
    /// Створює новий фільм
    /// </summary>
    /// <param name="dto">Дані нового фільму</param>
    /// <returns>Створений фільм і адреса, за якою його можна знайти</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
    public ActionResult<MovieDto> Create([FromBody] CreateMovieDto dto)
    {
        var created = _movieService.Create(dto); // created = новий фільм в MovieDto
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Видаляє фільм
    /// </summary>
    /// <param name="id">Ідентифікатор фільму</param>
    /// <returns>204, якщо видалено; 404, якщо такого фільму нема</returns>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete([FromRoute] int id)
    {
        var deleted = _movieService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Оновлює фільм
    /// </summary>
    /// <param name="id">Ідентифікатор фільму</param>
    /// <param name="dto">Нові дані фільму</param>
    /// <returns>204, якщо видалено; 404, якщо такого фільму нема</returns>
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update([FromRoute] int id, [FromBody] UpdateMovieDto dto)
    {
        var updated = _movieService.Update(id, dto);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

}
