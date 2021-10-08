using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.DBModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public MembershipController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        // POST api/<MembershipController>
        [HttpPost("create")]
        public async Task<ActionResult<Membership>> CreateMembership(Membership membership)
        {
            var result = await _repo.CreateMembership(membership);
           
            return Ok(result);
            
        }



        // DELETE api/<MembershipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
