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
    public class OrderStateServiceTests
    {
        [Fact]
        public void Add_ShouldAddOrderStateModel()
        {
            var mockRepo = new Mock<IOrderStateRepository>();
            var service = new OrderStateService(mockRepo.Object);
            var model = new OrderStateModel(1, "NewOrder");

            service.Add(model);

            mockRepo.Verify(r => r.Add(It.Is<OrderState>(o => o.Id == 1 && o.StateName == "NewOrder")), Times.Once);
        }

        [Fact]
        public void Delete_ShouldRemoveOrderStateModel()
        {
            var mockRepo = new Mock<IOrderStateRepository>();
            var service = new OrderStateService(mockRepo.Object);

            service.Delete(1);

            mockRepo.Verify(r => r.DeleteById(1), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnOrderStateModels()
        {
            var mockRepo = new Mock<IOrderStateRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<OrderState>
            {
                new OrderState(1, "State1"),
                new OrderState(2, "State2")
            });

            var service = new OrderStateService(mockRepo.Object);
            var result = service.GetAll().ToList();

            Assert.Equal(2, result.Count);
            Assert.IsType<OrderStateModel>(result.First());
            Assert.Equal("State1", ((OrderStateModel)result.First()).StateName);
        }
    }
}
