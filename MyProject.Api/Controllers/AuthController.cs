using Microsoft.AspNetCore.Mvc;
using MyProject.Core.Dtos.RequestDtos;
using MyProject.Core.Dtos.ResponseDtos;
using MyProject.Core.Models;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserRegisterRequestDto request)
    {
        var user = await authService.Register(request.Username, request.Password);
        return Ok(ToResponseDto(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginRequestDto request)
    {
        var token = await authService.Login(request.Username, request.Password);
        return Ok(token);
    }
    
    private static UserResponseDto ToResponseDto(User user)
    {
        return new UserResponseDto(user.Id, user.Username);
    }
}