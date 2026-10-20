using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure.Repositories
{
    public class TodoRepository
    {
        private readonly List<TodoItem> _todos;

        public TodoRepository()
        {
            // TODO: initialiseer de lijst
            _todos = new List<TodoItem>();
        }

        public List<TodoItem> GetAll()
        {
            return new List<TodoItem>(_todos);
        }

        public TodoItem Get(int id)
        {
            return _todos.Find(todo => todo.Id == id);
        }

        public void Add(TodoItem item)
        {
            item.Id = _todos.Count + 1;

            _todos.Add(item);
        }

        public bool Remove(TodoItem item)
        {
            // TODO: verwijder het item en return true/false
            return _todos.Remove(item);
        }


    }
}
