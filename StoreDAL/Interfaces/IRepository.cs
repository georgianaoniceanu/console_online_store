namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Provides data access operations for this entity.
/// </summary>
public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> GetAll(int pageNumber, int rowCount);

    TEntity GetById(int id);

    void Add(TEntity entity);

    void Delete(TEntity entity);

    void DeleteById(int id);

    void Update(TEntity entity);
}
