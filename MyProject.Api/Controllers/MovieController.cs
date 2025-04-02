using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Dtos.RequestDtos;
using MyProject.Api.Dtos.ResponseDtos;
using MyProject.Core.Exceptions;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;

namespace MyProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MovieController(IMovieRepository movieRepository) : ControllerBase
{
    [HttpGet("getMovies")]
    public async Task<ActionResult<List<MovieResponseDto>>> GetMovies()
    {
        var userId = GetUserId() ?? throw new UserIdentificationException();

        var movies = await movieRepository.GetByUserIdAsync(userId);
        return Ok(movies.Select(ToResponseDto));
    }

    [HttpPost("addMovie")]
    public async Task<ActionResult<MovieResponseDto>> AddMovie([FromBody] AddMovieRequestDto addMovieDto)
    {
        var userId = GetUserId() ?? throw new UserIdentificationException();

        var movie = new Movie
        {
            Title = addMovieDto.Title,
            Description = addMovieDto.Description,
            Assessment = addMovieDto.Assessment,
            IsViewed = false,
            UserId = userId
        };

        await movieRepository.AddAsync(movie);
        
        var addedItem = await movieRepository.GetByIdAsync(movie.Id);
        if (addedItem == null)
        {
            throw new ItemNotFoundException();
        }

        return Ok(ToResponseDto(addedItem));
    }

    [HttpGet("getMovie/{id:int}")]
    public async Task<ActionResult<MovieResponseDto>> GetMovie(int id)
    {
        var movie = await movieRepository.GetByIdAsync(id) ?? throw new ItemNotFoundException();

        return Ok(ToResponseDto(movie));
    }

    private int? GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claim, out var id) ? id : null;
    }

    private static MovieResponseDto ToResponseDto(Movie item)
    {
        return new MovieResponseDto(item.Id, item.Title, item.Description, item.IsViewed, item.Assessment);
    }
}