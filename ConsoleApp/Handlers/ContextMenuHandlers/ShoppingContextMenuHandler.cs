using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.Handlers.ContextMenuHandlers;

public class ShoppingContextMenuHandler : ContextMenuHandler
{
    public ShoppingContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
        : base(service, readModel)
    {
    }

    public void CreateOrder()
    {
        if (UserMenuController.CurrentUserId == 0)
        {
            Console.WriteLine("Trebuie sa fii autentificat pentru a plasa o comanda.");
            return;
        }

        Console.WriteLine("Input Product Id to order");
        var productId = ConsoleApp.Helpers.InputHelper.ReadInt();
        var product = this.service.GetById(productId) as ProductModel;
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine("Input quantity");
        var amount = ConsoleApp.Helpers.InputHelper.ReadInt();

        var orderService = UserMenuController.ServiceProvider.GetRequiredService<CustomerOrderService>();
        var detailService = UserMenuController.ServiceProvider.GetRequiredService<OrderDetailService>();

        var newOrderId = orderService.GetAll().Select(o => o.Id).DefaultIfEmpty(0).Max() + 1;
        var order = new CustomerOrderModel(
            newOrderId,
            DateTime.Now.ToString(CultureInfo.InvariantCulture),
            UserMenuController.CurrentUserId,
            1);
        orderService.Add(order);

        var newDetailId = detailService.GetAll().Select(d => d.Id).DefaultIfEmpty(0).Max() + 1;
        var detail = new OrderDetailModel(newDetailId, newOrderId, productId, product.UnitPrice, amount);
        detailService.Add(detail);

        Console.WriteLine($"Comanda #{newOrderId} a fost creata cu succes.");
    }

    public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                 (ConsoleKey.V, "View Details", this.GetItemDetails),
                 (ConsoleKey.A, "Add item to chart and create order", this.CreateOrder),
            };
        return array;
    }
}

