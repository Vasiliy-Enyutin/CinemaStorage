using Microsoft.EntityFrameworkCore;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;
using MyProject.DAL.Data;

namespace MyProject.DAL.Repositories;

public class TodoRepository(ApplicationDbContext context) : ITodoRepository
{
    public async Task<IEnumerable<TodoItem>> GetByUserIdAsync(int userId)
        => await context.TodoItems
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<TodoItem?> GetByIdAsync(int id)
        => await context.TodoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(TodoItem item)
    {
        await context.TodoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }
}