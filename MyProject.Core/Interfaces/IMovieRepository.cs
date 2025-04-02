using MyProject.Core.Models;

namespace MyProject.Core.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetByUserIdAsync(int userId);
    Task<Movie?> GetByIdAsync(int id);
    Task AddAsync(Movie item);
}