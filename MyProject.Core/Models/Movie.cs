namespace MyProject.Core.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsViewed { get; set; }
    public float? Assessment { get; set; }
    public float? AssessmentKinopoisk { get; set; }
    public int? Length { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}