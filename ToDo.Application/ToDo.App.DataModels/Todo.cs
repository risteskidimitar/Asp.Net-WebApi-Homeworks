using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.DataModels
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        public IEnumerable<SubTask> Subtasks { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Todo()
        {
            DateCreated = DateTime.Now;
        }
    }
}
