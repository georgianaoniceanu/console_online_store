using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ConsoleMenu;
using ConsoleMenu.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleApp1;

public enum UserRoles
{
    Guest,
    Administrator,
    RegistredCustomer,
}

public static class UserMenuController
{
    private static readonly Dictionary<UserRoles, Menu> RolesToMenu;
    private static int userId;
    private static UserRoles userRole;
    private static IServiceProvider serviceProvider;

    static UserMenuController()
    {
        userId = 0;
        userRole = UserRoles.Guest;
        RolesToMenu = new Dictionary<UserRoles, Menu>();
    }

    public static IServiceProvider ServiceProvider
    {
        get { return serviceProvider; }
    }

    public static int CurrentUserId
    {
        get { return userId; }
    }

    public static UserRoles CurrentUserRole
    {
        get { return userRole; }
    }

    public static void Start(IServiceProvider provider)
    {
        serviceProvider = provider;

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            RolesToMenu.Add(UserRoles.Guest, new GuestMainMenu().Create(context));
            RolesToMenu.Add(UserRoles.RegistredCustomer, new UserMainMenu().Create(context));
            RolesToMenu.Add(UserRoles.Administrator, new AdminMainMenu().Create(context));
        }

        ConsoleKey resKey;
        bool updateItems = true;
        do
        {
            resKey = RolesToMenu[userRole].RunOnce(ref updateItems);
        }
        while (resKey != ConsoleKey.Escape);
    }

    public static void Login()
    {
        Console.WriteLine("Username: ");
        var login = Console.ReadLine();
        Console.WriteLine("Password: ");
        var password = ReadPassword();

        try
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<UserService>();
            var user = service.Login(login ?? string.Empty, password ?? string.Empty);

            if (user == null)
            {
                Console.WriteLine("Username sau parola incorecte.");
                return;
            }

            userId = user.Id;
            userRole = MapRoleIdToUserRole(user.RoleId);
            Console.WriteLine($"Bine ai venit, {user.Name}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la autentificare: {ex.Message}");
        }
    }

    public static void Register()
    {
        Console.WriteLine("Nume: ");
        var name = Console.ReadLine();
        Console.WriteLine("Prenume: ");
        var lastName = Console.ReadLine();
        Console.WriteLine("Username: ");
        var login = Console.ReadLine();
        Console.WriteLine("Parola: ");
        var password = ReadPassword();

        try
        {
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<UserService>();
            var newId = service.GetAll().Select(u => u.Id).DefaultIfEmpty(0).Max() + 1;

            // 2 = Registered (see UserRole seed data)
            service.Add(new UserModel(newId, name ?? string.Empty, lastName ?? string.Empty, login ?? string.Empty, password ?? string.Empty, 2));
            Console.WriteLine("Inregistrare reusita. Te poti autentifica acum.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la inregistrare: {ex.Message}");
        }
    }

    public static void Logout()
    {
        userId = 0;
        userRole = UserRoles.Guest;
    }

    private static UserRoles MapRoleIdToUserRole(int roleId)
    {
        return roleId switch
        {
            1 => UserRoles.Administrator,
            2 => UserRoles.RegistredCustomer,
            _ => UserRoles.Guest,
        };
    }

    private static string ReadPassword()
    {
        var password = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password = password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                password += keyInfo.KeyChar;
            }
        }
        while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
}
