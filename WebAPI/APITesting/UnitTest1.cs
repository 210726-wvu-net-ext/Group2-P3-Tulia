using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Entities;
using Xunit;

namespace APITesting
{
    public class UnitTest1
    {
        [Fact]
        public void GetUserById()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Users.Add(new WebAPI.Models.Entities.User {
                    Id = 1,
                    Username = "Liam",
                    Password = "Password",
                    FirstName = "Liam",
                    LastName = "Sloan",
                    NumberGroups = 0,
                    Role = "user"
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                var user = new WebAPI.Models.DBModels.User(1, "Liam", "Sloan", "Liam");
                var result = repo.GetUserById(1);

                Assert.Equal(user.Username, result.Result.Username);
            }
        }

        [Fact]
        public void ReturnNullForInvalidUser()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Users.Add(new WebAPI.Models.Entities.User
                {
                    Id = 2,
                    Username = "Liam",
                    Password = "Password",
                    FirstName = "Liam",
                    LastName = "Sloan",
                    NumberGroups = 0,
                    Role = "user"
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                var user = new WebAPI.Models.DBModels.User(2, "greg", "Sloan", "Liam");
                var result = repo.GetUserById(1);

                Assert.NotEqual(user, result.Result);
            }
        }
    }
}
