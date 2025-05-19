using EduCenter.API.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace EduCenter.API.Features.Auth;
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        try
        {
            await _mediator.Send(command, ct);
            return Ok(); // NOTE: 200 OK, tokens in cookies
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
