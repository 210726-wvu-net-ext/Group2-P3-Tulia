using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
