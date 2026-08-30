using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Handlers.ContextMenuHandlers;
using ConsoleApp.Helpers;
using ConsoleApp1;
using ConsoleMenu;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleApp.Controllers
{
    public static class ProductController
    {
        public static void AddProduct()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            service.Add(InputHelper.ReadProductModel());
        }

        public static void UpdateProduct()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            service.Update(InputHelper.ReadProductModel());
        }

        public static void DeleteProduct()
        {
            Console.WriteLine("Input Product Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            service.Delete(id);
        }

        public static void ShowProduct()
        {
            Console.WriteLine("Input Product Id");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            Console.WriteLine(service.GetById(id));
        }

        public static void ShowAllProducts()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadProductModel), service.GetAll);
            menu.Run();
        }

        public static void ShowProductsGuest()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            var menu = new ContextMenu(
                new GuestContextMenuHandler(service, InputHelper.ReadProductModel).GenerateMenuItems,
                service.GetAll);
            menu.Run();
        }

        public static void ShowProductsForShopping()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductService>();
            var menu = new ContextMenu(
                new ShoppingContextMenuHandler(service, InputHelper.ReadProductModel).GenerateMenuItems,
                service.GetAll);
            menu.Run();
        }

        public static void AddCategory()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CategoryService>();
            service.Add(InputHelper.ReadCategoryiModel());
        }

        public static void UpdateCategory()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CategoryService>();
            service.Update(InputHelper.ReadCategoryiModel());
        }

        public static void DeleteCategory()
        {
            Console.WriteLine("Input Category Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CategoryService>();
            service.Delete(id);
        }

        public static void ShowAllCategories()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CategoryService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadCategoryiModel), service.GetAll);
            menu.Run();
        }

        public static void AddProductTitle()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductTitleService>();
            service.Add(InputHelper.ReadProductTitleModel());
        }

        public static void UpdateProductTitle()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductTitleService>();
            service.Update(InputHelper.ReadProductTitleModel());
        }

        public static void DeleteProductTitle()
        {
            Console.WriteLine("Input Product Title Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductTitleService>();
            service.Delete(id);
        }

        public static void ShowAllProductTitles()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ProductTitleService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadProductTitleModel), service.GetAll);
            menu.Run();
        }

        public static void AddManufacturer()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ManufacturerService>();
            service.Add(InputHelper.ReadManufacturerModel());
        }

        public static void UpdateManufacturer()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ManufacturerService>();
            service.Update(InputHelper.ReadManufacturerModel());
        }

        public static void DeleteManufacturer()
        {
            Console.WriteLine("Input Manufacturer Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<ManufacturerService>();
            service.Delete(id);
        }

        public static void ShowAllManufacturers()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<ManufacturerService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadManufacturerModel), service.GetAll);
            menu.Run();
        }
    }
}

