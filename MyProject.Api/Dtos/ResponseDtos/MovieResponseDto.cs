namespace MyProject.Api.Dtos.ResponseDtos;

public record MovieResponseDto(
    int Id, 
    string Title, 
    bool IsViewed,
    string? Description, 
    float? Assessment,
    float? AssessmentKinopoisk,
    int? Length);