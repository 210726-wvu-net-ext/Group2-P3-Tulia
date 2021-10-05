﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.DBModels;

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

        [HttpPost("create")]
        public string CreatePost(Post post)
        {
            return _repo.CreatePost(post);
        }

        [HttpGet("all")]
        public List<Post> GetAllPosts()
        {
            return _repo.GetAllPosts();
        }

        [HttpGet("{groupId}")]
        public List<Post> GetPostsFromGroup(int groupId)
        {
            return _repo.GetPostsFromGroup(groupId);
        }

        [HttpDelete("/delete/{postId}")]
        public ActionResult<Post> DeletePost(int postId)
        {
            var result = _repo.DeletePost(postId);
            if(result != null)
            {
                return Ok(result);
            } else
            {
                return StatusCode(404, "Post could not be found.");
            }
        }
    }
}
