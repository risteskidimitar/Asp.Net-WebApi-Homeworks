using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ToDo.App.DataAccess;
using ToDo.App.DataModels;
using ToDo.App.DtoModels;
using ToDo.App.Services.Exceptions;
using ToDo.App.Services.Interfaces;

namespace ToDo.App.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel Authenticate(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == username);
            if (user == null) throw new UserException($"User with username: {username} does not exists");

            var hashedPasword = HashPassword(password);
            if(user.Password != hashedPasword) throw new UserException("Incorrect password, try again");

            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userModel;
        }

        public void Register(RegisterModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.ConfirmPassword)) throw new UserException("Password is required");

            if (request.Password != request.ConfirmPassword) throw new UserException("Passwords does not match");

            if (string.IsNullOrWhiteSpace(request.FirstName)) throw new UserException("Firstname is required"); 

            if (string.IsNullOrWhiteSpace(request.LastName)) throw new UserException("Lastname is required");

            if (string.IsNullOrWhiteSpace(request.Username)) throw new UserException("Username is required");

            var hashedPassword = HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = hashedPassword
            };
            _userRepository.Insert(user);
        }

        private string HashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(md5data);
        }
    }
}
