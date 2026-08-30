namespace StoreBLL.Services
{
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

#pragma warning disable SA1600 // Elements should be documented
    /// <summary>
    /// Provides business logic services.
    /// </summary>
    public class CategoryService : ICrud
#pragma warning restore SA1600 // Elements should be documented
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public void Add(AbstractModel model)
        {
            var x = (CategoryModel)model;
            this.repository.Add(new Category(x.Id, x.Name));
        }

        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository.GetAll().Select(x => new CategoryModel(x.Id, x.Name));
        }

        public AbstractModel GetById(int id)
        {
            var res = this.repository.GetById(id);
            return new CategoryModel(res.Id, res.Name);
        }

        public void Update(AbstractModel model)
        {
            var x = (CategoryModel)model;
            this.repository.Update(new Category(x.Id, x.Name));
        }
    }
}
