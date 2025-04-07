using Microsoft.AspNetCore.Mvc;
using MyProject.Core.Dtos.RequestDtos;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Register(UserRegisterRequestDto request)
    {
        await authService.Register(request.Username, request.Password);
        return Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> Login(UserLoginRequestDto request)
    {
        var token = await authService.Login(request.Username, request.Password);
        // return Ok(token);
        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok();
    }
}