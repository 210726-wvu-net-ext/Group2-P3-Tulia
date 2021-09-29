﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Database_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public List<User> GetAllUsers()
        {
            return  _repo.GetAllUsers();
        }

        [HttpPost]
        public string CreateUser([FromBody] User user)
        {
            return _repo.CreateUser(user);
        }
    }
}
