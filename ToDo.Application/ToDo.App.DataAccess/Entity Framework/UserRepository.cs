using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DataModels;

namespace ToDo.App.DataAccess.Entity_Framework
{
    public class UserRepository : IRepository<User>
    {
        private readonly ToDoDbContext _context;
        public UserRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
           return _context.Users;
        }

        public void Insert(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }


        public void Remove(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
