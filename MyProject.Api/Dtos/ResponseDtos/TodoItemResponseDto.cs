namespace MyProject.Api.Dtos.ResponseDtos;

public record TodoItemResponseDto(
    int Id, 
    string Title, 
    string Description, 
    bool IsCompleted);