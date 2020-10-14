using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.App.DataAccess;
using ToDo.App.DataModels;
using ToDo.App.DtoModels;
using ToDo.App.Services.Exceptions;
using ToDo.App.Services.Interfaces;

namespace ToDo.App.Services
{
    public class SubTaskService : ISubtaskService
    {
        private readonly IRepository<Todo> _taskRepository;
        private readonly IRepository<SubTask> _subTaskRepository;

        public SubTaskService(IRepository<Todo> taskRepository, IRepository<SubTask> SubTaskRepository)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = SubTaskRepository;
        }

        public void AddSubtask(SubTaskDto request)
        {
            var task = _taskRepository.GetAll().FirstOrDefault(x => x.Id == request.TaskId);

            if (task == null) throw new TaskException("Task does not exist");

            if (string.IsNullOrWhiteSpace(request.Title)) throw new TaskException("Title is required");


            var subtask = new SubTask
            {
                Title = request.Title,
                TaskDescription = request.TaskDescription,
                TodoId = request.TaskId,
                IsCompleted = false
                
            };
            _subTaskRepository.Insert(subtask);
        }

        public void DeleteSubtask(int id, int taskId)
        {
            var subtask = _subTaskRepository.GetAll().FirstOrDefault(s => s.Id == id && s.TodoId == taskId);
            if (subtask == null) throw new TaskException("Try again, invalid task ID or subtask ID");

            _subTaskRepository.Remove(subtask);
        }

        public SubTaskDto GetSubTask(int id, int taskId)
        {
            var subtask = _subTaskRepository
                            .GetAll()
                            .FirstOrDefault(x => x.Id == id && x.TodoId == taskId);

            if (subtask == null) throw new TaskException("Subtask does not exists, try with different task or/and subtask ID");

            return new SubTaskDto
            {
                Id = subtask.Id,
                Title = subtask.Title,
                TaskDescription = subtask.TaskDescription,
                TaskId = subtask.TodoId,
                
            };
        }

        public IEnumerable<SubTaskDto> GetSubtasksOfTask(int taskId)
        {
            return _subTaskRepository
                    .GetAll()
                    .Where(x => x.TodoId == taskId)
                    .Select(x => new SubTaskDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TaskDescription = x.TaskDescription,
                        TaskId = x.TodoId,                  
                    });
        }
    }
}
