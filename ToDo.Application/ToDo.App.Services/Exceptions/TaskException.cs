using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.Services.Exceptions
{
    public class TaskException : Exception
    {
        public TaskException(string message)
            : base(message)
        {

        }
    }
}
