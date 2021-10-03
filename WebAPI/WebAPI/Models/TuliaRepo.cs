﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    public class TuliaRepo : ITuliaRepo
    {
        private readonly tuliadbContext _context;

        public TuliaRepo(tuliadbContext context)
        {
            _context = context;
        }

        public List<DBModels.User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            List<DBModels.User> userList = new List<DBModels.User>();

            foreach(var user in users)
            {
                userList.Add(new DBModels.User(user.Id, user.FirstName, user.LastName, user.Username));
            }

            return userList;
        }

        public async Task<DBModels.User> GetUserById(int id)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser != null)
            {
                return new DBModels.User(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Role);
            }
            return new DBModels.User();
        }

        public DBModels.User CreateUser(DBModels.User user)
        {
            try
            {
                var duplicateUsername = _context.Users.Single(u => u.Username == user.Username);
                return null;
            } catch(System.InvalidOperationException e)
            {
                _context.Users.Add(new Entities.User
                {
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = "user"
                });
                _context.SaveChanges();

                return new DBModels.User(0, user.FirstName, user.LastName, user.Username);
            }
        }

        public string CreateGroup(DBModels.Group group)
        {
            // check to see if that group name is taken already.
            try
            {
                var duplicateGroupName = _context.Groups.Single(g => g.GroupTitle == group.GroupTitle);
                return "A group with that title already exists";
            } catch(System.InvalidOperationException)
            {
                _context.Groups.Add(new Entities.Group
                {
                    GroupTitle = group.GroupTitle,
                    UserId = group.UserId,
                    Description = group.Description,
                    NumberMember = 1
                });
                _context.SaveChanges();

                return "Group created successfully";
            }
        }

        public List<DBModels.Group> GetAllGroups()
        {
            var groups = _context.Groups.ToList();
            List<DBModels.Group> listGroups = new List<DBModels.Group>();

            foreach(var group in groups)
            {
                listGroups.Add(new DBModels.Group(group.Id, group.UserId, group.NumberMember, group.GroupTitle, group.Description));
            }

            return listGroups;
        }

        //public DBModels.User LogIn(LoggedInUser user)
        //{
        //    // check to see if username exists
        //    try
        //    {
        //        var loginUser = _context.Users.Single(u => u.Username == user.username);
        //        
        //        //if that succeeds, make sure password fits
        //        if(user.password == loginUser.Password)
        //        {
        //            return new DBModels.User(loginUser.Id, loginUser.Username, loginUser.Password, loginUser.FirstName, loginUser.LastName,
        //                loginUser.Role, loginUser.NumberGroups);
        //        } else
        //        {
        //            return new DBModels.User(0, "error", "error", "error");
        //        }
        //    } catch (System.InvalidOperationException)
        //    {
        //        // username could not be found
        //        // return an "error" user object
        //        return new DBModels.User(0, "error", "error", "error");
        //    }
        //}

        public async Task<DBModels.User> LogIn(LoggedInUser user)
        {
            Entities.User foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.username && u.Password == user.password);

            if (foundUser != null)
            {
                DBModels.User loginUser = await GetUserById(foundUser.Id);
                return loginUser;
            }else
            return null;
        }

        // adds a comment to the database
        public DBModels.Comment CreateComment(DBModels.Comment comment)
        {
            _context.Comments.Add(new Entities.Comment
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Content = comment.Content,
                Time = comment.Time
            });
            _context.SaveChanges();
            return new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time);
        }

        // List all comments from a user
        public List<DBModels.Comment> ListCommentsFromUser(DBModels.User user)
        {
            // try to find the user
            try
            {
                var foundUser = _context.Users.Single(u => u.Username == user.Username);
                
                // if user is found, find user's comments
                try
                {
                    var comments = _context.Comments.Where(c => c.UserId == foundUser.Id).ToList();
                    List<DBModels.Comment> userComments = new List<DBModels.Comment>();

                    foreach(var comment in comments)
                    {
                        userComments.Add(new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time));
                    }

                    return userComments;
                } catch (System.InvalidOperationException)
                {
                    return null;
                }
            } catch(System.InvalidOperationException)
            {
                return null;
            }
        }

        // Create a new post
        public string CreatePost(DBModels.Post post)
        {
            try
            {
                _context.Posts.Add(new Entities.Post
                {
                    UserId = post.UserId,
                    Title = post.Title,
                    GroupId = post.GroupId,
                    Body = post.Body,
                    CreatedTime = post.CreatedTime
                });
                _context.SaveChanges();
                return "Post created successfully";
            } catch(System.InvalidOperationException)
            {
                return "There was an error creating this post";
            }
        }

        // see the last 15 posts
        public List<DBModels.Post> GetAllPosts()
        {
            var posts = _context.Posts.ToList();

            List<DBModels.Post> fetchedPosts = new List<DBModels.Post>();

            foreach(var post in posts)
            {
                fetchedPosts.Add(new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }

        // returns the last 10 posts from a specific group
        public List<DBModels.Post> GetPostsFromGroup(int groupId)
        {
            var posts = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            List<DBModels.Post> fetchedPosts = new List<DBModels.Post>();

            foreach (var post in posts)
            {
                fetchedPosts.Add(new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }

        // remove a group
        public string DeleteGroup(int groupId)
        {
            try
            {
                var group = _context.Groups.Single(g => g.Id == groupId);
                _context.Groups.Remove(group);
                _context.SaveChanges();
                return "Group removed";
            }
            catch (System.InvalidOperationException)
            {
                return "Error: That group could not be found";
            }
        }

        // displays comments from specific post from post id
        public List<DBModels.Comment> DisplayCommentsOnPost(int postId)
        {
            var comments = _context.Comments.Where(p => p.PostId == postId);
            List<DBModels.Comment> commentList = new List<DBModels.Comment>();

            foreach(var comment in comments)
            {
                commentList.Add(new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time));
            }

            return commentList;
        }
    }
}
