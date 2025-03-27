using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Dtos.RequestDtos;

public record TodoItemRequestDto(
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    string Title,
    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    string Description);