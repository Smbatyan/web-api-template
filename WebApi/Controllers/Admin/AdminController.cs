using Application.DTO.Response.Test;
using Application.Features.User.Commands.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Test admin endpoint
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("admin-endpoint")]
    public async Task<IActionResult> Get(string name)
    {
        var response = await _mediator.Send(new CreateUserV1Command(){ Name = name });
        
        await Task.Delay(100);
        
        return Ok(response);
    }
}