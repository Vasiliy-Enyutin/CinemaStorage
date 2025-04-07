namespace MyProject.Core.Models;

public class UserChallenge
{
    public int Id { get; set; }
    public int Progress { get; set; } // Текущий прогресс (например, 2/3)
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ChallengeId { get; set; }
    public Challenge Challenge { get; set; }
}

public enum ChallengeStatus
{
    Active,
    Completed,
    Archived
}