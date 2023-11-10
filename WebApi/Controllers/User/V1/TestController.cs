using Application.DTO.Response.Test;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "userV1")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Test admin endpoint
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("user-endpoint")]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(100);
        return Ok(new TestResponse(){Text = "text"});
    }
}