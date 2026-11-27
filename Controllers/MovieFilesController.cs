using Microsoft.AspNetCore.Mvc;
using MoviesApi.Application.Files;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/movies/{id:int}")]
public class MovieFilesController : ControllerBase
{
    private const long PosterRequestLimit = 3 * 1024 * 1024;
    private const long ScriptRequestLimit = 11 * 1024 * 1024;


    private readonly IFileStorageService _storage;

    public MovieFilesController(IFileStorageService storage)
    {
        _storage = storage;
    }


    [HttpPut("poster")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(PosterRequestLimit)]
    public async Task<IActionResult> UploadPoster(int id, IFormFile file)
    {
        await using var content = file.OpenReadStream();
        var upload = new UploadedFile(content, file.FileName, file.Length);
        // Валідація зображення
        var error = await FileValidator.ValidateAsync(upload, FileRules.Poster);
        if (error is not null)
        {
            ModelState.AddModelError(nameof(file), error);
            return ValidationProblem(ModelState);
        }
        // збереження зображення
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var path = await _storage.SaveAsync(content, "poster", extension, StorageZone.Public);

        // зберегти path в БД
        

        return Ok(new
        {
            path
        });
    }
}