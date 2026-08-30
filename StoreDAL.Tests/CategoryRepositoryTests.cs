using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using Xunit;

namespace StoreDAL.Tests
{
    public class CategoryRepositoryTests
    {
        private StoreDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new StoreDbContext(options, new TestDataFactory());
        }

        [Fact]
        public void Add_ShouldAddCategoryToDatabase()
        {
            using var context = GetContext();
            var repo = new CategoryRepository(context);
            var category = new Category { Id = 1, Name = "TestCategory" };

            repo.Add(category);

            Assert.Equal(1, context.Categories.Count());
            Assert.Equal("TestCategory", context.Categories.First().Name);
        }

        [Fact]
        public void Delete_ShouldRemoveCategory()
        {
            using var context = GetContext();
            var category = new Category { Id = 1, Name = "TestCategory" };
            context.Categories.Add(category);
            context.SaveChanges();

            var repo = new CategoryRepository(context);
            repo.Delete(category);

            Assert.Equal(0, context.Categories.Count());
        }

        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            using var context = GetContext();
            context.Categories.Add(new Category { Id = 1, Name = "Cat1" });
            context.Categories.Add(new Category { Id = 2, Name = "Cat2" });
            context.SaveChanges();

            var repo = new CategoryRepository(context);
            var result = repo.GetAll().ToList();

            Assert.Equal(2, result.Count);
        }
    }
}
