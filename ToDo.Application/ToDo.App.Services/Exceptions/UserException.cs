﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.App.Services.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        {

        }
    }
}
