using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.App.DataAccess;
using ToDo.App.DataModels;
using ToDo.App.DtoModels;
using ToDo.App.Services.Exceptions;
using ToDo.App.Services.Interfaces;

namespace ToDo.App.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Todo> _taskRepository;

        public TaskService(IRepository<User> userRepository, IRepository<Todo> taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }
        public void AddTask(TaskDto request)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == request.UserId);

            if (user == null) throw new TaskException("User does not exist");

            if (string.IsNullOrWhiteSpace(request.Title)) throw new TaskException("Title is required");
            
            if (request.DateCreated == null) throw new TaskException("Date is required");

            if (request.UserId <= 0) throw new TaskException("UserId is required and must be positive number");

            var task = new Todo
            {
                UserId = request.UserId,
                DateCreated = request.DateCreated,
                Title = request.Title,
                IsCompleted = false

            };
            _taskRepository.Insert(task);
        }

        public void DeleteTask(int id, int userId)
        {
            var task = _taskRepository
                       .GetAll()
                       .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (task == null) throw new TaskException("Task with that ID does not exists");

            _taskRepository.Remove(task);
        }

        public TaskDto GetTask(int id, int userId)
        {
            var task = _taskRepository
                       .GetAll()
                       .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (task == null) throw new TaskException("Task does not exists, try with different task id or/and UserId");

            return new TaskDto
            {
                Title = task.Title,
                DateCreated = task.DateCreated,
                IsCompleted = task.IsCompleted,
                UserId = task.UserId
            };
        }

        public IEnumerable<TaskDto> GetUserTasks(int userId)
        {
            return _taskRepository
                    .GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new TaskDto
                    {
                        UserId = x.UserId,
                        DateCreated = x.DateCreated,
                        IsCompleted = x.IsCompleted,
                        Title = x.Title,
                    });
        }
    }
}
