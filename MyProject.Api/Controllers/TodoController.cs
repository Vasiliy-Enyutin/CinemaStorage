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
public class TodoController(ITodoRepository todoRepository) : ControllerBase
{
    [HttpGet("getTodos")]
    public async Task<ActionResult<List<TodoItemResponseDto>>> GetTodos()
    {
        var userId = GetUserId() ?? throw new UserIdentificationException();

        var todos = await todoRepository.GetByUserIdAsync(userId);
        return Ok(todos.Select(ToResponseDto));
    }

    [HttpPost("createTodo")]
    public async Task<ActionResult<TodoItemResponseDto>> CreateTodo([FromBody] CreateTodoItemRequestDto createTodoDto)
    {
        // TODO валидация
        if (!ModelState.IsValid)
        {
            throw new ApiException("Invalid request data");
        }

        var userId = GetUserId() ?? throw new UserIdentificationException();

        var todoItem = new TodoItem
        {
            Title = createTodoDto.Title,
            Description = createTodoDto.Description,
            IsCompleted = false,
            UserId = userId
        };

        await todoRepository.AddAsync(todoItem);
        
        var addedItem = await todoRepository.GetByIdAsync(todoItem.Id);
        if (addedItem == null)
        {
            throw new ItemNotFoundException();
        }

        return Ok(ToResponseDto(addedItem));
    }

    [HttpGet("getTodo/{id:int}")]
    public async Task<ActionResult<TodoItemResponseDto>> GetTodo(int id)
    {
        var todoItem = await todoRepository.GetByIdAsync(id) ?? throw new ItemNotFoundException();

        return Ok(ToResponseDto(todoItem));
    }

    private int? GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claim, out var id) ? id : null;
    }

    private static TodoItemResponseDto ToResponseDto(TodoItem item)
    {
        return new TodoItemResponseDto(item.Id, item.Title, item.Description, item.IsCompleted);
    }
}