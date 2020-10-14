using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DtoModels;

namespace ToDo.App.Services.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetUserTasks(int userId);
        TaskDto GetTask(int id, int userId);
        void AddTask(TaskDto request);
        void DeleteTask(int id, int userId);
    }
}
