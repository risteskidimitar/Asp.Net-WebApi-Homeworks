using System;
using System.Collections.Generic;
using System.Text;
using ToDo.App.DtoModels;

namespace ToDo.App.Services.Interfaces
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
        void Register(RegisterModel request);
    }
}
