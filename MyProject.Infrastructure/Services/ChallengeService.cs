using MyProject.Core.Interfaces;
using MyProject.Core.Models;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Infrastructure.Services;

public class ChallengeService(IChallengeRepository challengeRepository) : IChallengeService
{
    public async Task CreateChallengeAsync(Challenge challenge)
    {
        await challengeRepository.AddAsync(challenge);
    }
}