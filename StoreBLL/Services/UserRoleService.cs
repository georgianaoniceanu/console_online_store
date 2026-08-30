namespace StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

/// <summary>
/// Provides business logic services.
/// </summary>
public class UserRoleService : ICrud
{
    private readonly IUserRoleRepository repository;

    public UserRoleService(IUserRoleRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (UserRoleModel)model;
        this.repository.Add(new UserRole(x.Id, x.RoleName));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new UserRoleModel(x.Id, x.RoleName));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new UserRoleModel(res.Id, res.RoleName);
    }

    public void Update(AbstractModel model)
    {
        var x = (UserRoleModel)model;
        this.repository.Update(new UserRole(x.Id, x.RoleName));
    }
}
