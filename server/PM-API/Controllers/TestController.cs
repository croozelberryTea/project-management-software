using Microsoft.AspNetCore.Mvc;

namespace PM_API.Controllers;

[ApiController]
[Route("Test")]
public class TestController : ControllerBase
{
    public  TestController()
    {
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}