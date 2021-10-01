using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.DBModels;

namespace WebAPI.Models
{
    public interface ITuliaRepo
    {
        public List<DBModels.User> GetAllUsers();

        public User CreateUser(User user);

        public string CreateGroup(Group group);

        public List<Group> GetAllGroups();

        public User LogIn(LoggedInUser user);

        public Comment CreateComment(Comment comment);

        public List<Comment> ListCommentsFromUser(User user);

        public string CreatePost(Post post);

        public List<Post> GetAllPosts();

        public List<Post> GetPostsFromGroup(int groupId);

        public string DeleteGroup(int groupId);

        public List<Comment> DisplayCommentsOnPost(int postId);
    }
}
