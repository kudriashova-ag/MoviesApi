using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesApi.Application.Files;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/movies/{id:int}")]
public class MovieFilesController : ControllerBase
{
    private const long PosterRequestLimit = 3 * 1024 * 1024;
    private const long ScriptRequestLimit = 11 * 1024 * 1024;



    [HttpPut("poster")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(PosterRequestLimit)]
    public async Task<IActionResult> UploadPoster(int id, IFormFile file)
    {
        await using var content = file.OpenReadStream();
        var upload = new UploadedFile(content, file.FileName, file.Length);

        var error = await FileValidator.ValidateAsync(upload, FileRules.Poster);
        if (error is not null)
        {
            ModelState.AddModelError(nameof(file), error);
            return ValidationProblem(ModelState);
        }




        return Ok(new
        {
            file.Name,
            file.FileName,
            file.ContentType,
            file.Length
        });
    }
}