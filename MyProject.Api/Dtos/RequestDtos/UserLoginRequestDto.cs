using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Dtos.RequestDtos;

public record UserLoginRequestDto(
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 4)] 
    string Username,
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 4)] 
    string Password);
