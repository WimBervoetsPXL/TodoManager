using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Models;
using TodoManager.Infrastructure;
using TodoManager.Infrastructure.Repositories;

namespace TodoManager.Application.Services
{
    public class TodoService
    {
        private readonly TodoJsonRepository _repository;

        public TodoService()
        {
            // TODO: maak de repository aan
            _repository = new TodoJsonRepository();
        }

        public List<TodoItem> GetTodos()
        {
            // TODO: return alle todos via repository
            return _repository.GetAll();
        }

        public void AddTodo(string title, string? description, DateTime dueDate)
        {
            TodoItem? duplicate = _repository
                .GetAll()
                .Find(todo => 
                    todo.Title.Equals(title.Trim(), StringComparison.OrdinalIgnoreCase));

            if(duplicate is not null)
            {
                throw new InvalidOperationException("Deze todo bestaat al.");
            }

            TodoItem todo = new TodoItem(title, description, dueDate);
            _repository.Add(todo);
        }

        public void CompleteTodo(TodoItem todo)
        {
            // TODO: zoek het item in de repository (TIP: `Get(todo.id)`)
            TodoItem existing = _repository.Get(todo.Id);

            if (existing is null)
                return;

            // TODO: roep `MarkAsCompleted()` aan
            existing.MarkAsCompleted();
        }

        public void DeleteTodo(TodoItem todo)
        {
            if (todo.DueDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("Dit item kan niet worden verwijderd omdat de duedate in het verleden ligt.");
            }

            if (todo.IsCompleted)
            {
                throw new InvalidOperationException("Dit item kan niet worden verwijderd omdat het al voltooid is.");
            }

            TodoItem existing = _repository.Get(todo.Id);
            _repository.Remove(existing);
        }


    }
}
