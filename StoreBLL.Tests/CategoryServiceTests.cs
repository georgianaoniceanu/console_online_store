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
    public class CategoryServiceTests
    {
        [Fact]
        public void Add_ShouldAddCategoryModel()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var service = new CategoryService(mockRepo.Object);
            var model = new CategoryModel(1, "TestCategory");

            service.Add(model);

            mockRepo.Verify(r => r.Add(It.Is<Category>(c => c.Id == 1 && c.Name == "TestCategory")), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveCategoryModel()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            var service = new CategoryService(mockRepo.Object);

            service.Delete(1);

            mockRepo.Verify(r => r.DeleteById(1), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnCategoryModels()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Category>
            {
                new Category { Id = 1, Name = "Cat1" },
                new Category { Id = 2, Name = "Cat2" }
            });

            var service = new CategoryService(mockRepo.Object);
            var result = service.GetAll().ToList();

            Assert.Equal(2, result.Count);
            Assert.IsType<CategoryModel>(result.First());
            Assert.Equal("Cat1", ((CategoryModel)result.First()).Name);
        }
    }
}
