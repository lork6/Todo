using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepositroy _repository;

        public TodoItemsController(ITodoRepositroy repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            
            return Ok(_repository.GetAllTodoItems());       
        }
        [HttpGet]
        [Route("todo")]
        public ActionResult<IEnumerable<TodoItem>> GetTodo()
        {
            return Ok(_repository.GetAllTodo());
        }
        [HttpGet]
        [Route("in-progress")]
        public ActionResult<IEnumerable<TodoItem>> GetInProgress()
        {
            return Ok(_repository.GetAllProgress());
        }
        [HttpGet]
        [Route("done")]
        public ActionResult<IEnumerable<TodoItem>> GetCompleted()
        {
            return Ok(_repository.GetAllDone());
        }
        [HttpGet]
        [Route("postponed")]
        public ActionResult<IEnumerable<TodoItem>> GetPostponed()
        {
            return Ok(_repository.GetAllPostponed());
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(long id)
        {
            var todoItem = _repository.GetTodoItemsById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }


        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItem todoItem)
        {
            //Console.WriteLine(todoItem.Id);

            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            _repository.UpdateTodoItems(todoItem);

            try
            {
                _repository.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {

            _repository.CreateTodoItems(todoItem);
            _repository.SaveChanges();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTodoItem(long id)
        {
            var todoItem = _repository.GetTodoItemsById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _repository.DeleteTodoItems(todoItem);
            _repository.SaveChanges();

            return todoItem;
        }

        

    }
}
