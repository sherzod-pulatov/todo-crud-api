using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> GetToDo()
        {
            return Ok(await _context.ToDos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDoByID(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo is null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> AddToDo(ToDo newTodo)
        {
            if (newTodo is null)
                return BadRequest();

            _context.ToDos.Add(newTodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDo), new { id = newTodo.Id }, newTodo);
        }

        // Update todo by id without returning anything
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, ToDo updatedTodo)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo is null)
                return NotFound();

            todo.Task = updatedTodo.Task;
            todo.IsDone = updatedTodo.IsDone;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Deleting by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
                return BadRequest("ToDo not found.");
            
            _context.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
