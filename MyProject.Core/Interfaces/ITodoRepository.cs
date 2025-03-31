using MyProject.Core.Models;

namespace MyProject.Core.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetByUserIdAsync(int userId);
    Task<TodoItem?> GetByIdAsync(int id);
    Task AddAsync(TodoItem item);
}