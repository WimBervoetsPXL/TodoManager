using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure
{
    public class TodoJsonRepository
    {
        const string filename = "todos.json";

        public TodoJsonRepository()
        {
            if(!File.Exists(filename))
            {
                File.WriteAllText(filename, "[ ]");
            }
        }

        public List<TodoItem> GetAll()
        {
            string jsonContent = File.ReadAllText(filename);
            List<TodoItem>? items = JsonSerializer.Deserialize<List<TodoItem>>(jsonContent);

            return items ?? new List<TodoItem>();
        }

        public TodoItem Get(int id)
        {
            List<TodoItem> todos = GetAll();

            TodoItem? existing = todos.Find(item => item.Id == id);

            if (existing is null)
                throw new ArgumentException($"Item met id {id} bestaat niet.");

            return existing;
        }

        public void Add(TodoItem item)
        {
            List<TodoItem> allTodos = this.GetAll();

            TodoItem? existing = allTodos.MaxBy(t => t.Id);

            if(existing is null)
            {
                item.Id = 1;
            }
            else
            {
                item.Id = existing.Id + 1;
            }

            allTodos.Add(item);

            string jsonContent = JsonSerializer.Serialize(allTodos);
            File.WriteAllText(filename, jsonContent);
        }

        public bool Remove(TodoItem item)
        {
            List<TodoItem> allTodos = this.GetAll();
            bool result = allTodos.RemoveAll(todo => todo.Id == item.Id) > 0;

            string jsonContent = JsonSerializer.Serialize(allTodos);
            File.WriteAllText(filename, jsonContent);

            return result;
        }

        public void Update(TodoItem item)
        {
            List<TodoItem> allTodos = this.GetAll();

            //TODO: update item in allTodos:
            int index = allTodos.FindIndex(todo => todo.Id == item.Id);

            if (index < 0)
                throw new ArgumentException();
            
            allTodos[index] = item;

            string jsonContent = JsonSerializer.Serialize(allTodos);
            File.WriteAllText(filename, jsonContent);
        }
    }
}
