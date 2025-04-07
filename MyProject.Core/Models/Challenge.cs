namespace MyProject.Core.Models;

public class Challenge
{
    public int Id { get; set; }
    public string Title { get; set; } // "Посмотри 3 фэнтези-фильма 90-х"
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ChallengeStatus Status { get; set; } = ChallengeStatus.Active;
    public List<ChallengeCriteria> Criterias { get; set; } = [];
}