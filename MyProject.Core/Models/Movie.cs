namespace MyProject.Core.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsViewed { get; set; } = false;
    public int Assessment { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}