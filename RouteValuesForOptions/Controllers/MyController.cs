using Microsoft.AspNetCore.Mvc;

namespace RouteValuesForOptions.Controllers;

[ApiController]
[Route("api")]
public class MyController :
    ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(
        string id)
    {
        return this.Ok();
    }
}
