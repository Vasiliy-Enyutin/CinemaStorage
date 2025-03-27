using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Dtos.RequestDtos;
using MyProject.Api.Dtos.ResponseDtos;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;

namespace MyProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoRepository todoRepository) : ControllerBase
{
    [HttpGet("getTodos")]
    public async Task<ActionResult<IEnumerable<TodoItemResponseDto>>> GetTodos()
    {
        try
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest($"UserId {userId} not found");
            }

            var todos = await todoRepository.GetByUserIdAsync(userId.Value);
            return Ok(todos.Select(ToResponseDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ErrorResult(ex));
        }
    }

    [HttpPost("createTodo")]
    public async Task<IActionResult> CreateTodo([FromBody] TodoItemRequestDto todoDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest($"UserId {userId} not found");
            }

            var todoItem = new TodoItem
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsCompleted = false,
                UserId = userId.Value
            };

            await todoRepository.AddAsync(todoItem);
            return CreatedAtAction(nameof(GetTodo), new { id = todoItem.Id }, ToResponseDto(todoItem));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ErrorResult(ex, "Error creating todo item"));
        }
    }

    [HttpGet("getTodo")]
    public async Task<ActionResult<TodoItemResponseDto>> GetTodo(int id)
    {
        try
        {
            var todo = await todoRepository.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new { Message = "Todo item not found" });
            }
            return Ok(ToResponseDto(todo));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error retrieving todo item", Details = ex.Message });
        }
    }

    #region Helpers
    private int? GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claim, out var id) ? id : null;
    }

    private static TodoItemResponseDto ToResponseDto(TodoItem item)
    {
        return new TodoItemResponseDto(item.Id, item.Title, item.Description, item.IsCompleted);
    }

    private static object ErrorResult(Exception ex, string message = "Internal server error")
    {
        return new { Message = message, Details = ex.Message };
    }    
    
    #endregion
}