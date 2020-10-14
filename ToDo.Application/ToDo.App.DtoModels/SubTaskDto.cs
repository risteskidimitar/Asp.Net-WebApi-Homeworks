using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.DtoModels
{
    public class SubTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public int TaskId { get; set; }
    }
}
