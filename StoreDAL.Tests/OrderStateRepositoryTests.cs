using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using Xunit;

namespace StoreDAL.Tests
{
    public class OrderStateRepositoryTests
    {
        private StoreDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new StoreDbContext(options, new TestDataFactory());
        }

        [Fact]
        public void Add_ShouldAddOrderStateToDatabase()
        {
            using var context = GetContext();
            var repo = new OrderStateRepository(context);
            var state = new OrderState { Id = 1, StateName = "New" };

            repo.Add(state);

            Assert.Equal(1, context.OrderStates.Count());
            Assert.Equal("New", context.OrderStates.First().StateName);
        }

        [Fact]
        public void Delete_ShouldRemoveOrderState()
        {
            using var context = GetContext();
            var state = new OrderState { Id = 1, StateName = "New" };
            context.OrderStates.Add(state);
            context.SaveChanges();

            var repo = new OrderStateRepository(context);
            repo.Delete(state);

            Assert.Equal(0, context.OrderStates.Count());
        }
    }
}
