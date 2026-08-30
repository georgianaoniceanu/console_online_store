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
public class OrderDetailService : ICrud
{
    private readonly IOrderDetailRepository repository;

    public OrderDetailService(IOrderDetailRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (OrderDetailModel)model;
        this.repository.Add(new OrderDetail(x.Id, x.OrderId, x.ProductId, x.Price, x.ProductAmount));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll()
            .Select(x => new OrderDetailModel(x.Id, x.OrderId, x.ProductId, x.Price, x.ProductAmount));
    }

    public IEnumerable<AbstractModel> GetByOrderId(int orderId)
    {
        return this.repository.GetByOrderId(orderId)
            .Select(x => new OrderDetailModel(x.Id, x.OrderId, x.ProductId, x.Price, x.ProductAmount));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        if (res == null)
        {
            return null;
        }

        return new OrderDetailModel(res.Id, res.OrderId, res.ProductId, res.Price, res.ProductAmount);
    }

    public void Update(AbstractModel model)
    {
        var x = (OrderDetailModel)model;
        this.repository.Update(new OrderDetail(x.Id, x.OrderId, x.ProductId, x.Price, x.ProductAmount));
    }
}
