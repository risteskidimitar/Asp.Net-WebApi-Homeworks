using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DataAccess;
using ToDo.App.DataAccess.Entity_Framework;
using ToDo.App.DataModels;
using ToDo.App.Services.Interfaces;

namespace ToDo.App.Services
{
    public static class DiModule
    {
        public static IServiceCollection RegisterModule(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ToDoDbContext>(x =>
            x.UseSqlServer(connectionString));
            //data access
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Todo>, TaskRepository>();
            services.AddTransient<IRepository<SubTask>, SubtaskRepository>();
            //services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ISubtaskService, SubTaskService>();

            return services;
        }
    }
}
