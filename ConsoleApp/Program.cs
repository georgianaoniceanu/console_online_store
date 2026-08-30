using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

namespace ConsoleApp1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            var services = new ServiceCollection();

            // Register the data factory required by StoreDbContext
            services.AddSingleton<AbstractDataFactory, TestDataFactory>();

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Register Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderStateRepository, OrderStateRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTitleRepository, ProductTitleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            // Register Services
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerOrderService>();
            services.AddScoped<ManufacturerService>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<OrderStateService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductTitleService>();
            services.AddScoped<UserService>();
            services.AddScoped<UserRoleService>();

            var serviceProvider = services.BuildServiceProvider();

            // Initialize DB
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
                dbContext.Database.EnsureCreated();
            }

            UserMenuController.Start(serviceProvider);
        }
    }
}