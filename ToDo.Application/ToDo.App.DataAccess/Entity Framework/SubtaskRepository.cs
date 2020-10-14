using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DataModels;

namespace ToDo.App.DataAccess.Entity_Framework
{
    public class SubtaskRepository : IRepository<SubTask>
    {
        private readonly ToDoDbContext _context;
        public SubtaskRepository(ToDoDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SubTask> GetAll()
        {
            return _context.Subtasks;
        }
        public void Insert(SubTask entity)
        {
            _context.Subtasks.Add(entity);
            _context.SaveChanges();
        }


        public void Remove(SubTask entity)
        {
            _context.Subtasks.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(SubTask entity)
        {
            _context.Subtasks.Update(entity);
            _context.SaveChanges();
        }
    }
}
