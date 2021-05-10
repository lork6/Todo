using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public interface ITodoRepositroy
    {
        bool SaveChanges();

        IEnumerable<TodoItem> GetAllTodoItems();
        TodoItem GetTodoItemsById(long id);
        void CreateTodoItems(TodoItem cmd);
        void UpdateTodoItems(TodoItem cmd);
        void DeleteTodoItems(TodoItem cmd);
        IEnumerable<TodoItem> GetAllTodo();
        IEnumerable<TodoItem> GetAllProgress();
        IEnumerable<TodoItem> GetAllDone();
        IEnumerable<TodoItem> GetAllPostponed();

    }
}
