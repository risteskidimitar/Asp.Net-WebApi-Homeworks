using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.DtoModels
{
    public class TaskDto
    {
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public TaskDto()
        {
            DateCreated = DateTime.Now;
        }
    }
}
