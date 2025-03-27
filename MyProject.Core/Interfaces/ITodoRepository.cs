using MyProject.Core.Models;

namespace MyProject.Core.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetByUserIdAsync(int userId);
    Task<TodoItem?> GetByIdAsync(int id);
    Task AddAsync(TodoItem item);
}