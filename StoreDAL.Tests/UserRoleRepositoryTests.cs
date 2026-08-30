using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using Xunit;

namespace StoreDAL.Tests
{
    public class UserRoleRepositoryTests
    {
        private StoreDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new StoreDbContext(options, new TestDataFactory());
        }

        [Fact]
        public void Add_ShouldAddUserRoleToDatabase()
        {
            using var context = GetContext();
            var repo = new UserRoleRepository(context);
            var role = new UserRole { Id = 1, RoleName = "Admin" };

            repo.Add(role);

            Assert.Equal(1, context.UserRoles.Count());
            Assert.Equal("Admin", context.UserRoles.First().RoleName);
        }

        [Fact]
        public void Delete_ShouldRemoveUserRole()
        {
            using var context = GetContext();
            var role = new UserRole { Id = 1, RoleName = "Admin" };
            context.UserRoles.Add(role);
            context.SaveChanges();

            var repo = new UserRoleRepository(context);
            repo.Delete(role);

            Assert.Equal(0, context.UserRoles.Count());
        }
    }
}
