using MyProject.Core.Models;

namespace MyProject.Core.Interfaces;

public interface IChallengeRepository
{
    Task AddAsync(Challenge challenge);
}