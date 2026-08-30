namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Provides data access operations for this entity.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    User GetByLogin(string login);
}
