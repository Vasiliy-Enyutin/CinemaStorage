namespace MyProject.Core.Dtos.RequestDtos.Challenges;

public record AddChallengeRequestDto(
    string Title,
    string Description,
    List<ChallengeCriteriaRequestDto> Criterias,
    DateTime EndDate);