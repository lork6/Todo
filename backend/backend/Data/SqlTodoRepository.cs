using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class SqlTodoRepository : ITodoRepositroy
    {
        private readonly TodoContext _context;
        public SqlTodoRepository(TodoContext context)
        {
            _context = context;
        }
        public void CreateTodoItems(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.TodoItems.Add(item);
        }

        public void DeleteTodoItems(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _context.TodoItems.Remove(item);
        }

        public IEnumerable<TodoItem> GetAllDone()
        {
            return _context.TodoItems.Where(i => i.complete == 2).OrderBy(o => o.order).ToList();
        }

        public IEnumerable<TodoItem> GetAllPostponed()
        {
            return _context.TodoItems.Where(i => i.complete == 3).OrderBy(o => o.order).ToList();
        }

        public IEnumerable<TodoItem> GetAllProgress()
        {
            return _context.TodoItems.Where(i => i.complete == 1).OrderBy(o => o.order).ToList();
        }

        public IEnumerable<TodoItem> GetAllTodo()
        {
            return _context.TodoItems.Where(i => i.complete == 0).OrderBy(o => o.order).ToList();
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem GetTodoItemsById(long id)
        {
            return _context.TodoItems.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateTodoItems(TodoItem next)
        {
            var prev =  _context.TodoItems.Where(a => a.Id == next.Id).FirstOrDefault();

            UpdateOrders(prev, next);

            prev.name = next.name;
            prev.date = next.date;
            prev.description = next.description;
            prev.order = next.order;
            prev.complete = next.complete;
        }

        private void UpdateOrders(TodoItem prev, TodoItem next)
        {
            // honnan lista
            var todoitems = _context.TodoItems.Where(i => i.complete == prev.complete).Where(i => next.complete == prev.complete ? next.order >= i.order && i.order > prev.order : i.order > prev.order).ToList();
            foreach (TodoItem todo in todoitems)
            {
                todo.order -= 1;
                if (todo.order < 0)
                {
                    todo.order = 0;
                }
                _context.Entry(todo).State = EntityState.Modified;
            }
            //hova lista
            var todoitems2 = _context.TodoItems.Where(i => i.complete == next.complete).Where(i => next.complete == prev.complete ? i.order >= next.order && prev.order > i.order : i.order >= next.order).ToList();
            foreach (TodoItem todo in todoitems2)
            {
                todo.order += 1;
                _context.Entry(todo).State = EntityState.Modified;
            }
        }
    }
}
