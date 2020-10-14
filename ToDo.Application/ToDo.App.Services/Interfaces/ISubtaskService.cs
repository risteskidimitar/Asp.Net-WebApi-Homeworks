using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DtoModels;

namespace ToDo.App.Services.Interfaces
{
    public interface ISubtaskService
    {
        IEnumerable<SubTaskDto> GetSubtasksOfTask (int taskId);
        SubTaskDto GetSubTask(int id, int taskId);
        void AddSubtask(SubTaskDto request);
        void DeleteSubtask(int id, int taskId);
    }
}
