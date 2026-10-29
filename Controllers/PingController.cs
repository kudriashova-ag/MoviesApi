using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers;

[ApiController]
[Route("")]
public class PingController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Hello World";
    }

    [HttpGet("ping")]
    public string Ping()
    {
        return "pong";
    }


}