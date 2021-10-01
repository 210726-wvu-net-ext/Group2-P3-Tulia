﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Controller_Models;
using WebAPI.Models.Database_Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public UserController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        [HttpPost("register")]
        public User CreateUser(User user)
        {
            return _repo.CreateUser(user);
        }

        [HttpPost("login")]
        public User Login(string username, string password)
        {
            return _repo.LogIn(new LoggedInUser(username, password));
        }
    }
}
