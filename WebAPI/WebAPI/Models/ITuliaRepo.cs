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
        public List<User> GetAllUsers();

        public Task<User> GetUserById(int id);

        public User CreateUser(User user);

        public Task<User> UpdateUser(int id, User user);
        public Task<UserWithGroup> GetUserWithGroup(int id);

        public Group CreateGroup(Group group);

        public List<Group> GetAllGroups();

        public Task<Group> UpdateGroup(int id, Group group);

        public Task<Membership> CreateMembership(Membership membership);

        public Task<User> LogIn(LoggedInUser user);

        public Task<bool> DeleteUserById(int id);

        public Comment CreateComment(Comment comment);

        public List<Comment> ListCommentsFromUser(User user);

        public Post CreatePost(Post post);

        public List<Post> GetAllPosts();

        public List<Post> GetPostsFromGroup(int groupId);

        public Group DeleteGroup(int groupId);

        public List<Comment> DisplayCommentsOnPost(int postId);

        public Comment DeleteComment(int commentId);

        public Post DeletePost(int postId);
    }
}
