using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    /// <summary>
    /// Repository implementation for data access.
    /// </summary>
    public class CustomerOrderRepository : AbstractRepository, ICustomerOrderRepository
    {
        public CustomerOrderRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(CustomerOrder entity)
        {
            this.context.CustomerOrders.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(CustomerOrder entity)
        {
            this.context.CustomerOrders.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.context.CustomerOrders.Find(id);
            if (entity != null)
            {
                this.context.CustomerOrders.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<CustomerOrder> GetAll()
        {
            return this.context.CustomerOrders
                .Include(o => o.User)
                .Include(o => o.State)
                .Include(o => o.Details)
                .ToList();
        }

        public IEnumerable<CustomerOrder> GetAll(int pageNumber, int rowCount)
        {
            return this.context.CustomerOrders
                .Include(o => o.User)
                .Include(o => o.State)
                .Include(o => o.Details)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public CustomerOrder GetById(int id)
        {
            return this.context.CustomerOrders
                .Include(o => o.User)
                .Include(o => o.State)
                .Include(o => o.Details)
                .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<CustomerOrder> GetByUserId(int userId)
        {
            return this.context.CustomerOrders
                .Include(o => o.State)
                .Include(o => o.Details)
                .Where(o => o.UserId == userId)
                .ToList();
        }

        public void Update(CustomerOrder entity)
        {
            this.context.CustomerOrders.Update(entity);
            this.context.SaveChanges();
        }
    }
}
