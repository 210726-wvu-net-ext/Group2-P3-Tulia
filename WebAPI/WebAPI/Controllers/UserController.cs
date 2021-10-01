using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.DBModels;

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
        public ActionResult<User> CreateUser(User user)
        {
            var result = _repo.CreateUser(user);
            if(result != null)
            {
                return result;
            } else
            {
                return StatusCode(403, null);
            }
        }

        [HttpPost("login")]
        public User Login(string username, string password)
        {
            return _repo.LogIn(new LoggedInUser(username, password));
        }
    }
}
