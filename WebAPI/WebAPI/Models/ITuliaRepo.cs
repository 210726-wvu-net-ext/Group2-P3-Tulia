using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Database_Models;

namespace WebAPI.Models
{
    public interface ITuliaRepo
    {
        public List<Database_Models.User> GetAllUsers();

        public string CreateUser(User user);

        public string CreateGroup(Group group);

        public List<Group> GetAllGroups();
    }
}
