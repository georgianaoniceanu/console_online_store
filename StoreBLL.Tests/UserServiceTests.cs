using System.Collections.Generic;
using System.Linq;
using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using Xunit;

namespace StoreBLL.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void Add_ShouldAddUserModel()
        {
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            var model = new UserModel(1, "John", "Doe", "jdoe", "pass", 2);

            service.Add(model);

            mockRepo.Verify(r => r.Add(It.Is<User>(u => u.Id == 1 && u.Name == "John")), Times.Once);
        }

        [Fact]
        public void Login_ShouldReturnUserModel_WhenCredentialsAreValid()
        {
            var mockRepo = new Mock<IUserRepository>();
            // Note: Hash for "pass" is assumed to be what HashPassword generates. 
            // We use the actual method logic: SHA256 of "pass" -> Base64
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes("pass"));
            var hashedPass = System.Convert.ToBase64String(bytes);

            var expectedUser = new User(1, "John", "Doe", "jdoe", hashedPass, 2);
            mockRepo.Setup(r => r.GetByLogin("jdoe")).Returns(expectedUser);

            var service = new UserService(mockRepo.Object);
            var result = service.Login("jdoe", "pass");

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            var mockRepo = new Mock<IUserRepository>();
            var expectedUser = new User(1, "John", "Doe", "jdoe", "hashedpass", 2);
            mockRepo.Setup(r => r.GetByLogin("jdoe")).Returns(expectedUser);

            var service = new UserService(mockRepo.Object);
            var result = service.Login("jdoe", "wrongpass");

            Assert.Null(result);
        }
    }
}
