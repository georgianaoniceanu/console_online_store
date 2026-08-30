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
public class ManufacturerService : ICrud
{
    private readonly IManufacturerRepository repository;

    public ManufacturerService(IManufacturerRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (ManufacturerModel)model;
        this.repository.Add(new Manufacturer(x.Id, x.Name));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new ManufacturerModel(x.Id, x.Name));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new ManufacturerModel(res.Id, res.Name);
    }

    public void Update(AbstractModel model)
    {
        var x = (ManufacturerModel)model;
        this.repository.Update(new Manufacturer(x.Id, x.Name));
    }
}
