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
    public class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        private readonly DbSet<Category> dbSet;

        public CategoryRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Category>();
        }

        public void Add(Category entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.dbSet.Find(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return this.dbSet.ToList();
        }

        public IEnumerable<Category> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public Category GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(Category entity)
        {
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
