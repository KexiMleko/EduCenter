using EduCenter.API.Base;
using EduCenter.API.Features.Auth.RefreshTokens;
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
    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshTokens(CancellationToken ct)
    {
        try
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("No refresh token was found in the cookie.");
            await _mediator.Send(new RefreshTokensCommand(refreshToken), ct);
            return Ok();
        }
        catch
        {
            return Unauthorized();
        }
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        var token = Request.Cookies["RefreshToken"];
        if (string.IsNullOrEmpty(token))
            return BadRequest("Refresh token is missing.");
        await _mediator.Send(new LogoutCommand(token), ct);
        return NoContent();
    }
}
