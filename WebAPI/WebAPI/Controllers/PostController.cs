using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Database_Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public PostController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("~/api/[controller]/create/{post}")]
        public string CreatePost(Post post)
        {
            return _repo.CreatePost(post);
        }

        [HttpGet("~/api/[controller]/all")]
        public List<Post> GetAllPosts()
        {
            return _repo.GetAllPosts();
        }

        [HttpGet("~/api/[controller]/{groupId}")]
        public List<Post> GetPostsFromGroup(int groupId)
        {
            return _repo.GetPostsFromGroup(groupId);
        }
    }
}
