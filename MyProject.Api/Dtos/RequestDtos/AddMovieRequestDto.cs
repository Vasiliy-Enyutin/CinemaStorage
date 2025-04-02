namespace MyProject.Api.Dtos.RequestDtos;

public record AddMovieRequestDto(string Title, bool IsViewed, string? Description, float? Assessment, float? AssessmentKinopoisk, int? Length);