﻿using System;
using System.Collections.Generic;

namespace SEDC.WebApi.NoteApp.DataModels.DbModels
{
    public partial class Users
    {
        public Users()
        {
            Notes = new HashSet<Notes>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Notes> Notes { get; set; }
    }
}
