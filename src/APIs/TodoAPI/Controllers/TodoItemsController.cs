using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TodoItemsController(TodoContext context) : ControllerBase
{
    private readonly TodoContext _context = context;

    private bool TodoItemExists(long id) => _context.TodoItems.Any(t => t.Id == id);

    private TodoItemDTO TodoItemToDTO(TodoItem todoItem) 
        => new TodoItemDTO(todoItem.Id, todoItem.Name, todoItem.IsComplete);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        => await _context.TodoItems.Select(t => TodoItemToDTO(t)).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        return todoItem is not null ?
            Ok(TodoItemToDTO(todoItem)) :
            NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
    {
        if (ModelState.IsValid)
        {
            var todoItem = new TodoItem { 
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, TodoItemToDTO(todoItem));
        }

        return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
    {
        if (ModelState.IsValid && id == todoItemDTO.Id)
        {
            var todoItem = new TodoItem {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                    return NotFound();
                
                throw;
            }
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem is null)
            return NotFound();
        
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}