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
    public class CustomerOrderServiceTests
    {
        [Fact]
        public void Add_ShouldAddCustomerOrderModel()
        {
            var mockRepo = new Mock<ICustomerOrderRepository>();
            var service = new CustomerOrderService(mockRepo.Object);
            var model = new CustomerOrderModel(1, "2023-01-01", 1, 1);

            service.Add(model);

            mockRepo.Verify(r => r.Add(It.Is<CustomerOrder>(o => o.Id == 1 && o.UserId == 1)), Times.Once);
        }

        [Fact]
        public void ChangeOrderState_ShouldUpdateOrderState_WhenTransitionIsValid()
        {
            var mockRepo = new Mock<ICustomerOrderRepository>();
            var order = new CustomerOrder(1, "2023-01-01", 1, 1); // UserId 1, StateId 1
            mockRepo.Setup(r => r.GetById(1)).Returns(order);

            var service = new CustomerOrderService(mockRepo.Object);

            // From NewOrder (1) to Confirmed (4) -> Valid transition (AllowedTransitions[1] = {2,3,4})
            service.ChangeStateByAdmin(1, 4);

            mockRepo.Verify(r => r.Update(It.Is<CustomerOrder>(o => o.OrderStateId == 4)), Times.Once);
        }

        [Fact]
        public void ChangeOrderState_ShouldThrowException_WhenTransitionIsInvalid()
        {
            var mockRepo = new Mock<ICustomerOrderRepository>();
            var order = new CustomerOrder(1, "2023-01-01", 1, 4); // StateId 4 (Confirmed)
            mockRepo.Setup(r => r.GetById(1)).Returns(order);

            var service = new CustomerOrderService(mockRepo.Object);

            // From Confirmed (4) to NewOrder (1) -> Invalid transition
            Assert.Throws<System.InvalidOperationException>(() => service.ChangeStateByAdmin(1, 1));
        }
    }
}
