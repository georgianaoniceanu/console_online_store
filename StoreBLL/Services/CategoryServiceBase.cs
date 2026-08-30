namespace StoreBLL.Services
{
    using StoreDAL.Data;
    using StoreDAL.Interfaces;
    using StoreDAL.Repository;

    /// <summary>
    /// Provides business logic services.
    /// </summary>
    public class CategoryServiceBase
    {
        private readonly ICategoryRepository repository;

        public CategoryServiceBase(StoreDbContext context)
        {
            this.repository = new CategoryRepository(context);
        }
    }
}
