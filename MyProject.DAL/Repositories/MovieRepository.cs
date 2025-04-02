using Microsoft.EntityFrameworkCore;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;
using MyProject.DAL.Data;

namespace MyProject.DAL.Repositories;

public class MovieRepository(ApplicationDbContext context) : IMovieRepository
{
    public async Task<List<Movie>> GetByUserIdAsync(int userId)
        => await context.Movies
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Movie?> GetByIdAsync(int id)
        => await context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Movie item)
    {
        await context.Movies.AddAsync(item);
        await context.SaveChangesAsync();
    }
}