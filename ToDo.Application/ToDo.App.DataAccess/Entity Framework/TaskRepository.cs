using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DataModels;

namespace ToDo.App.DataAccess.Entity_Framework
{
    public class TaskRepository : IRepository<Todo>
    {
        private readonly ToDoDbContext _context;
        public TaskRepository(ToDoDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Todo> GetAll()
        {
            return _context.Tasks;
        }

        public void Insert(Todo entity)
        {
            _context.Tasks.Add(entity);
            _context.SaveChanges();
        }


        public void Remove(Todo entity)
        {
            _context.Tasks.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Todo entity)
        {
            _context.Tasks.Update(entity);
            _context.SaveChanges();
        }
    }
}
