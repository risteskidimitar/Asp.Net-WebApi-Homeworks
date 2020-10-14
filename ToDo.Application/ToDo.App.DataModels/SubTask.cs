using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.DataModels
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }

        public int TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
