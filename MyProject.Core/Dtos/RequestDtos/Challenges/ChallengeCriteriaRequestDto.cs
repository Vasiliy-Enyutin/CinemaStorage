namespace MyProject.Core.Dtos.RequestDtos.Challenges;

public record ChallengeCriteriaRequestDto(
    string PropertyName,
    string Operator,
    string Value);