using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Controller_Models;
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

        public List<Database_Models.User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            List<Database_Models.User> userList = new List<Database_Models.User>();

            foreach(var user in users)
            {
                userList.Add(new Database_Models.User(user.Id, user.FirstName, user.LastName, user.Username));
            }

            return userList;
        }

        public string CreateUser(Database_Models.User user)
        {
            try
            {
                var duplicateUsername = _context.Users.Single(u => u.Username == user.Username);
                return "that username is already taken.";
            } catch(System.InvalidOperationException)
            {
                _context.Users.Add(new Entities.User
                {
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
                _context.SaveChanges();

                return "user created successfully.";
            }
        }

        public string CreateGroup(Database_Models.Group group)
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

        public List<Database_Models.Group> GetAllGroups()
        {
            var groups = _context.Groups.ToList();
            List<Database_Models.Group> listGroups = new List<Database_Models.Group>();

            foreach(var group in groups)
            {
                listGroups.Add(new Database_Models.Group(group.Id, group.UserId, group.NumberMember, group.GroupTitle, group.Description));
            }

            return listGroups;
        }

        public Database_Models.User LogIn(LoggedInUser user)
        {
            // check to see if username exists
            try
            {
                var loginUser = _context.Users.Single(u => u.Username == user.username);
                
                //if that succeeds, make sure password fits
                if(user.password == loginUser.Password)
                {
                    return new Database_Models.User(loginUser.Id, loginUser.Username, loginUser.Password, loginUser.FirstName, loginUser.LastName,
                        loginUser.Role, loginUser.NumberGroups);
                } else
                {
                    return new Database_Models.User(0, "error", "error", "error");
                }
            } catch (System.InvalidOperationException)
            {
                // username could not be found
                // return an "error" user object
                return new Database_Models.User(0, "error", "error", "error");
            }
        }

        // adds a comment to the database
        public Database_Models.Comment CreateComment(Database_Models.Comment comment)
        {
            _context.Comments.Add(new Entities.Comment
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Content = comment.Content,
                Time = comment.Time
            });
            _context.SaveChanges();
            return new Database_Models.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time);
        }

        // List all comments from a user
        public List<Database_Models.Comment> ListCommentsFromUser(Database_Models.User user)
        {
            // try to find the user
            try
            {
                var foundUser = _context.Users.Single(u => u.Username == user.Username);
                
                // if user is found, find user's comments
                try
                {
                    var comments = _context.Comments.Where(c => c.UserId == foundUser.Id).ToList();
                    List<Database_Models.Comment> userComments = new List<Database_Models.Comment>();

                    foreach(var comment in comments)
                    {
                        userComments.Add(new Database_Models.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time));
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
        public string CreatePost(Database_Models.Post post)
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
        public List<Database_Models.Post> GetAllPosts()
        {
            var posts = _context.Posts.ToList();

            List<Database_Models.Post> fetchedPosts = new List<Database_Models.Post>();

            foreach(var post in posts)
            {
                fetchedPosts.Add(new Database_Models.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }

        // returns the last 10 posts from a specific group
        public List<Database_Models.Post> GetPostsFromGroup(int groupId)
        {
            var posts = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            List<Database_Models.Post> fetchedPosts = new List<Database_Models.Post>();

            foreach (var post in posts)
            {
                fetchedPosts.Add(new Database_Models.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }
    }
}
