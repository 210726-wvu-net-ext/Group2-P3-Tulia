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
    public class GroupController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public GroupController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<Group> GetAllGroups()
        {
            return _repo.GetAllGroups();
        }

        [HttpPost]
        public string CreateGroup(Group group)
        {
            return _repo.CreateGroup(group);
        }
    }
}