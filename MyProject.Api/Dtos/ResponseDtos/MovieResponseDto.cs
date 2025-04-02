namespace MyProject.Api.Dtos.ResponseDtos;

public record MovieResponseDto(
    int Id, 
    string Title, 
    string Description, 
    bool IsViewed,
    int Assessment);