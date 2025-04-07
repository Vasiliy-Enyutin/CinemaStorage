namespace MyProject.Core.Dtos.ResponseDtos.Challenges;

public record ChallengeResponseDto(
    int Id,
    string Title,
    string Description,
    int Progress,
    bool IsCompleted,
    DateTime EndDate);