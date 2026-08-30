namespace ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenuHandlers;
using ConsoleApp.Helpers;
using ConsoleMenu;
using StoreDAL.Data;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Models;
using StoreBLL.Services;

public static class UserController
{
    public static void AddUser()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserService>();
        service.Add(InputHelper.ReadUserModel());
    }

    public static void UpdateUser()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserService>();
        service.Update(InputHelper.ReadUserModel());
    }

    public static void DeleteUser()
    {
        Console.WriteLine("Input User Id to delete");
        var id = ConsoleApp.Helpers.InputHelper.ReadInt();
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserService>();
        service.Delete(id);
    }

    public static void ShowUser()
    {
        Console.WriteLine("Input User Id");
        var id = ConsoleApp.Helpers.InputHelper.ReadInt();
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserService>();
        Console.WriteLine(service.GetById(id));
    }

    public static void ShowAllUsers()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserService>();
        var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadUserModel), service.GetAll);
        menu.Run();
    }

    public static void AddUserRole()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserRoleService>();
        service.Add(InputHelper.ReadUserRoleModel());
    }

    public static void UpdateUserRole()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserRoleService>();
        service.Update(InputHelper.ReadUserRoleModel());
    }

    public static void DeleteUserRole()
    {
        Console.WriteLine("Input User Role Id to delete");
        var id = ConsoleApp.Helpers.InputHelper.ReadInt();
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserRoleService>();
        service.Delete(id);
    }

    public static void ShowAllUserRoles()
    {
        var service = UserMenuController.ServiceProvider.GetRequiredService<UserRoleService>();
        var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadUserRoleModel), service.GetAll);
        menu.Run();
    }
}

