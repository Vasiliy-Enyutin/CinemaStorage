using MyProject.Core.Interfaces;
using MyProject.Core.Models;
using MyProject.DAL.Data;

namespace MyProject.DAL.Repositories;

public class ChallengeRepository(ApplicationDbContext context) : IChallengeRepository
{
    public async Task AddAsync(Challenge challenge)
    {
        await context.Challenges.AddAsync(challenge);
        await context.SaveChangesAsync();
    }
}