using System;
using System.Collections.Generic;
using System.Text;

namespace TodoManager.Domain.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; }
        public string? Description { get; }
        public DateTime DueDate { get; }
        public bool IsCompleted { get; private set; } = false;
        public DateTime? CompletedAt { get; private set; } = null;

        public TodoItem(string title, string? description, DateTime dueDate)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Titel mag niet leeg zijn.");
            }

            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
            CompletedAt = null;
        }

        public void MarkAsCompleted()
        {
            if (IsCompleted)
            {
                return;
                // Hierdoor wordt CompletedAt niet telkens overschreven
            }

            this.IsCompleted = true;
            this.CompletedAt = DateTime.Now;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[Done]" : "[Open]";
            return $"{status} {Title} (due {DueDate:dd/MM/yyyy})";
        }
    }
}
