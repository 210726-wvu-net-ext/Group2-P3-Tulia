﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Controller_Models
{
    public class LoggedInUser
    {
        public string username { get; private set; }
        public string password { get; private set; }
    }
}
