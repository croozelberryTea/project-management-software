using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PM_API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
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