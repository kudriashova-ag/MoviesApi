using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers;

[ApiController]
[Route("")]
public class PingController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public PingController(IWebHostEnvironment env)
    {
        _env = env;
    }


    [HttpGet]
    public string Get()
    {
        return "Hello World";
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(new
        {
            message = "MoviesApi is running",
            environment = _env.EnvironmentName
        });
    }


}