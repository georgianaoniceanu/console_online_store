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
public class ProductTitleService : ICrud
{
    private readonly IProductTitleRepository repository;

    public ProductTitleService(IProductTitleRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (ProductTitleModel)model;
        this.repository.Add(new ProductTitle(x.Id, x.Title, x.CategoryId));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new ProductTitleModel(x.Id, x.Title, x.CategoryId));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new ProductTitleModel(res.Id, res.Title, res.CategoryId);
    }

    public void Update(AbstractModel model)
    {
        var x = (ProductTitleModel)model;
        this.repository.Update(new ProductTitle(x.Id, x.Title, x.CategoryId));
    }
}
