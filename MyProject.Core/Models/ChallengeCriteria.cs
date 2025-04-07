namespace MyProject.Core.Models;

public class ChallengeCriteria
{
    public int Id { get; set; }
    public string PropertyName { get; set; } // "Year", "Genre"
    public string Operator { get; set; } // "==", ">="
    public string Value { get; set; } // "1990", "Fantasy"
    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; }
}