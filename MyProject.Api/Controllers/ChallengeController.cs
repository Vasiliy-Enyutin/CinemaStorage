using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Core.Dtos.RequestDtos.Challenges;
using MyProject.Core.Dtos.ResponseDtos.Challenges;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;

namespace MyProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChallengeController(IChallengeRepository challengeRepository) : ControllerBase
{
    [HttpPost("addChallenge")]
    public ActionResult<ChallengeResponseDto> AddChallenge(AddChallengeRequestDto addChallengeRequestDto)
    {
        var criterias = new List<ChallengeCriteria>();
        foreach (var criteria in addChallengeRequestDto.Criterias)
        {
            criterias.Add(new ChallengeCriteria
            {
                PropertyName = criteria.PropertyName,
                Operator = criteria.Operator,
                Value = criteria.Value
            });
        }
        
        var challenge = new Challenge
        {
            Title = addChallengeRequestDto.Title,
            Description = addChallengeRequestDto.Description,
            StartDate = 
            Criterias = criterias
        };
    }
}