using Microsoft.EntityFrameworkCore;
using MyProject.Core.Models;

namespace MyProject.DAL.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<UserChallenge> UserChallenges => Set<UserChallenge>();
    public DbSet<ChallengeCriteria> ChallengeCriterias => Set<ChallengeCriteria>();
}