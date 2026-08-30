using StoreDAL.Data.InitDataFactory;
using Xunit;

namespace StoreDAL.Tests
{
    public class TestDataFactoryTests
    {
        [Fact]
        public void GetCategoryData_ReturnsExpectedCategories()
        {
            // Arrange
            var factory = new TestDataFactory();

            // Act
            var categories = factory.GetCategoryData();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(14, categories.Length);
            Assert.Contains(categories, c => c.Name == "fruits");
        }

        [Fact]
        public void GetUserData_ReturnsAdminAndMary()
        {
            var factory = new TestDataFactory();
            var users = factory.GetUserData();

            Assert.NotNull(users);
            Assert.Contains(users, u => u.Login == "admin");
            Assert.Contains(users, u => u.Login == "mary");
        }

        [Fact]
        public void GetProductData_ReturnsProducts()
        {
            var factory = new TestDataFactory();
            var products = factory.GetProductData();

            Assert.NotNull(products);
            Assert.True(products.Length > 0);
        }
    }
}
