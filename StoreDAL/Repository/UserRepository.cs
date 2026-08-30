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
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(StoreDbContext context)
            : base(context)
        {
        }

        public void Add(User entity)
        {
            this.context.Users.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(User entity)
        {
            this.context.Users.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.context.Users.Find(id);
            if (entity != null)
            {
                this.context.Users.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.Include(u => u.Role).ToList();
        }

        public IEnumerable<User> GetAll(int pageNumber, int rowCount)
        {
            return this.context.Users
                .Include(u => u.Role)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public User GetById(int id)
        {
            return this.context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

        public User GetByLogin(string login)
        {
            return this.context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == login);
        }

        public void Update(User entity)
        {
            this.context.Users.Update(entity);
            this.context.SaveChanges();
        }
    }
}
