namespace ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Models;

internal static class InputHelper
{
    public static CategoryModel ReadCategoryiModel()
    {
        Console.WriteLine("Input Category Id");
        var id = ReadInt();
        Console.WriteLine("Input Category Name");
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        return new CategoryModel(id, name);
    }

    public static ManufacturerModel ReadManufacturerModel()
    {
        Console.WriteLine("Input Manufacturer Id");
        var id = ReadInt();
        Console.WriteLine("Input Manufacturer Name");
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        return new ManufacturerModel(id, name);
    }

    public static ProductTitleModel ReadProductTitleModel()
    {
        Console.WriteLine("Input Product Title Id");
        var id = ReadInt();
        Console.WriteLine("Input Title");
        var title = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(title);
        Console.WriteLine("Input Category Id");
        var categoryId = ReadInt();
        return new ProductTitleModel(id, title, categoryId);
    }

    public static ProductModel ReadProductModel()
    {
        Console.WriteLine("Input Product Id");
        var id = ReadInt();
        Console.WriteLine("Input Product Title Id");
        var titleId = ReadInt();
        Console.WriteLine("Input Manufacturer Id");
        var manufacturerId = ReadInt();
        Console.WriteLine("Input Description");
        var description = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(description);
        Console.WriteLine("Input Unit Price");
        var price = ReadDecimal();
        return new ProductModel(id, titleId, manufacturerId, description, price);
    }

    public static UserModel ReadUserModel()
    {
        Console.WriteLine("Input User Id");
        var id = ReadInt();
        Console.WriteLine("Input First Name");
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        Console.WriteLine("Input Last Name");
        var lastName = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(lastName);
        Console.WriteLine("Input Login");
        var login = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(login);
        Console.WriteLine("Input Password");
        var password = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(password);
        Console.WriteLine("Input Role Id (1=Admin, 2=Registered, 3=Guest)");
        var roleId = ReadInt();
        return new UserModel(id, name, lastName, login, password, roleId);
    }

    public static CustomerOrderModel ReadCustomerOrderModel()
    {
        Console.WriteLine("Input Order Id");
        var id = ReadInt();
        Console.WriteLine("Input User Id");
        var userId = ReadInt();
        Console.WriteLine("Input Order State Id");
        var stateId = ReadInt();
        var operationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        return new CustomerOrderModel(id, operationTime, userId, stateId);
    }

    public static OrderDetailModel ReadOrderDetailModel()
    {
        Console.WriteLine("Input Order Detail Id");
        var id = ReadInt();
        Console.WriteLine("Input Order Id");
        var orderId = ReadInt();
        Console.WriteLine("Input Product Id");
        var productId = ReadInt();
        Console.WriteLine("Input Price");
        var price = ReadDecimal();
        Console.WriteLine("Input Amount");
        var amount = ReadInt();
        return new OrderDetailModel(id, orderId, productId, price, amount);
    }

    public static OrderStateModel ReadOrderStateModel()
    {
        Console.WriteLine("Input State Id");
        var id = ReadInt();
        Console.WriteLine("Input State Name");
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        return new OrderStateModel(id, name);
    }

    public static UserRoleModel ReadUserRoleModel()
    {
        Console.WriteLine("Input User Role Id");
        var id = ReadInt();
        Console.WriteLine("Input User Role Name");
        var name = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(name);
        return new UserRoleModel(id, name);
    }

    public static int ReadInt()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }
            Console.WriteLine("Valoare invalida. Te rog sa introduci un numar intreg:");
        }
    }

    public static decimal ReadDecimal()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            Console.WriteLine("Valoare invalida. Te rog sa introduci un numar valid (ex: 10.5):");
        }
    }
}
