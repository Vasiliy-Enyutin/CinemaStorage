using MyProject.Core.Models;

namespace MyProject.Infrastructure.Interfaces;

public interface IChallengeService
{
    public Task CreateChallengeAsync(Challenge challenge);
}