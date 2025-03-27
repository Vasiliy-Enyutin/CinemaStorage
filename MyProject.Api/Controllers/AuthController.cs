using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Dtos.RequestDtos;
using MyProject.Core.Models;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegisterRequestDto request)
    {
        try
        {
            var user = await authService.Register(request.Username, request.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginRequestDto request)
    {
        try
        {
            var token = await authService.Login(request.Username, request.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
        
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}