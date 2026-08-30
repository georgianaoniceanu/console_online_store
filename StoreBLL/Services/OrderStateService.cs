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
public class OrderStateService : ICrud
{
    private readonly IOrderStateRepository repository;

    public OrderStateService(IOrderStateRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (OrderStateModel)model;
        this.repository.Add(new OrderState(x.Id, x.StateName));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new OrderStateModel(x.Id, x.StateName));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new OrderStateModel(res.Id, res.StateName);
    }

    public void Update(AbstractModel model)
    {
        var x = (OrderStateModel)model;
        this.repository.Update(new OrderState(x.Id, x.StateName));
    }
}
