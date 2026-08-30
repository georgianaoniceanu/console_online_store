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
    public class OrderDetailRepository : AbstractRepository, IOrderDetailRepository
    {
        public OrderDetailRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(OrderDetail entity)
        {
            this.context.OrderDetails.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(OrderDetail entity)
        {
            this.context.OrderDetails.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.context.OrderDetails.Find(id);
            if (entity != null)
            {
                this.context.OrderDetails.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return this.context.OrderDetails.Include(d => d.Product).ToList();
        }

        public IEnumerable<OrderDetail> GetAll(int pageNumber, int rowCount)
        {
            return this.context.OrderDetails
                .Include(d => d.Product)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public OrderDetail GetById(int id)
        {
            return this.context.OrderDetails.Include(d => d.Product).FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId)
        {
            return this.context.OrderDetails
                .Include(d => d.Product)
                .Where(d => d.OrderId == orderId)
                .ToList();
        }

        public void Update(OrderDetail entity)
        {
            this.context.OrderDetails.Update(entity);
            this.context.SaveChanges();
        }
    }
}
