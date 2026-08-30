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
    public static class ShopController
    {
        public static void AddOrder()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            service.Add(InputHelper.ReadCustomerOrderModel());
        }

        public static void UpdateOrder()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            service.Update(InputHelper.ReadCustomerOrderModel());
        }

        public static void DeleteOrder()
        {
            Console.WriteLine("Input Order Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            service.Delete(id);
        }

        public static void ShowOrder()
        {
            Console.WriteLine("Input Order Id");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            Console.WriteLine(service.GetById(id));
        }

        public static void ShowAllOrders()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            var menu = new ContextMenu(
                new OrderContextMenuHandler(service, InputHelper.ReadCustomerOrderModel).GenerateMenuItems,
                service.GetAll);
            menu.Run();
        }

        public static void ShowMyOrders()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            var menu = new ContextMenu(
                new OrderContextMenuHandler(service, InputHelper.ReadCustomerOrderModel).GenerateMenuItems,
                () => service.GetByUserId(UserMenuController.CurrentUserId));
            menu.Run();
        }

        public static void CancelMyOrder()
        {
            Console.WriteLine("Input Order Id to cancel");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            try
            {
                service.CancelByUser(id);
                Console.WriteLine("Comanda a fost anulata.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Apasa orice tasta pentru a continua...");
            Console.ReadKey();
        }

        public static void ConfirmMyOrderDelivery()
        {
            Console.WriteLine("Input Order Id to confirm");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            try
            {
                service.ConfirmDeliveryByClient(id);
                Console.WriteLine("Primirea comenzii a fost confirmata.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Apasa orice tasta pentru a continua...");
            Console.ReadKey();
        }

        public static void CancelOrderByAdmin()
        {
            Console.WriteLine("Input Order Id to cancel");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            try
            {
                service.ChangeStateByAdmin(id, 3); // 3 = Cancelled by administrator
                Console.WriteLine("Comanda a fost anulata de administrator.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Apasa orice tasta pentru a continua...");
            Console.ReadKey();
        }

        public static void ChangeOrderStatusByAdmin()
        {
            Console.WriteLine("Input Order Id");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            Console.WriteLine("Input new Order State Id");
            var newStateId = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
            try
            {
                service.ChangeStateByAdmin(id, newStateId);
                Console.WriteLine("Statusul comenzii a fost actualizat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Apasa orice tasta pentru a continua...");
            Console.ReadKey();
        }

        public static void AddOrderDetails()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<OrderDetailService>();
            service.Add(InputHelper.ReadOrderDetailModel());
        }

        public static void UpdateOrderDetails()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<OrderDetailService>();
            service.Update(InputHelper.ReadOrderDetailModel());
        }

        public static void DeleteOrderDetails()
        {
            Console.WriteLine("Input Order Detail Id to delete");
            var id = ConsoleApp.Helpers.InputHelper.ReadInt();
            var service = UserMenuController.ServiceProvider.GetRequiredService<OrderDetailService>();
            service.Delete(id);
        }

        public static void ShowAllOrderDetails()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<OrderDetailService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadOrderDetailModel), service.GetAll);
            menu.Run();
        }

        public static void ProcessOrder()
        {
            ShowAllOrders();
        }

        public static void ShowAllOrderStates()
        {
            var service = UserMenuController.ServiceProvider.GetRequiredService<OrderStateService>();
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadOrderStateModel), service.GetAll);
            menu.Run();
        }
    }
}

