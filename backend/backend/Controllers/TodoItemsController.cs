using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        [HttpGet]
        [Route("todo")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodo()
        {
            return await _context.TodoItems.Where(i => i.complete == 0).OrderBy(o => o.order).ToListAsync();
        }
        [HttpGet]
        [Route("in-progress")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetInProgress()
        {
            return await _context.TodoItems.Where(i => i.complete == 1).OrderBy(o => o.order).ToListAsync();
        }
        [HttpGet]
        [Route("done")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompleted()
        {
            return await _context.TodoItems.Where(i => i.complete == 2).OrderBy(o => o.order).ToListAsync();
        }
        [HttpGet]
        [Route("postponed")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetPostponed()
        {
            return await _context.TodoItems.Where(i => i.complete == 3).OrderBy(o => o.order).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            //Console.WriteLine(todoItem.Id);
         
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var item = await _context.TodoItems.Where(a => a.Id == id).FirstOrDefaultAsync();

            // honnan lista
            var todoitems = await _context.TodoItems.Where(i => i.complete == item.complete).Where(i => todoItem.complete == item.complete? todoItem.order >= i.order && i.order > item.order: i.order > item.order).ToListAsync();
            foreach (TodoItem todo in todoitems)
            {
                todo.order -= 1;
                if(todo.order < 0)
                {
                    todo.order = 0;
                }
                _context.Entry(todo).State = EntityState.Modified;
            }
            //hova lista
            var todoitems2 = await _context.TodoItems.Where(i => i.complete == todoItem.complete).Where(i => todoItem.complete == item.complete ? i.order >= todoItem.order && item.order > i.order: i.order >= todoItem.order).ToListAsync();
            foreach (TodoItem todo in todoitems2)
            {
                todo.order += 1;
                _context.Entry(todo).State = EntityState.Modified;
            }
            item.name = todoItem.name;
            item.date = todoItem.date;
            item.description = todoItem.description;
            item.order = todoItem.order;
            item.complete = todoItem.complete;
            try
            {
                await _context.SaveChangesAsync();
            

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
           
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
