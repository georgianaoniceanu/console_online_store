using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

public class AdminMainMenu : AbstractMenuCreator
{
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", UserMenuController.Logout),
                (ConsoleKey.F2, "Manage products", ProductController.ShowAllProducts),
                (ConsoleKey.F3, "Manage categories", ProductController.ShowAllCategories),
                (ConsoleKey.F4, "Manage product titles", ProductController.ShowAllProductTitles),
                (ConsoleKey.F5, "Manage manufacturers", ProductController.ShowAllManufacturers),
                (ConsoleKey.F6, "Show order list / change status", ShopController.ShowAllOrders),
                (ConsoleKey.F7, "Cancel order", ShopController.CancelOrderByAdmin),
                (ConsoleKey.F8, "Change order status (quick)", ShopController.ChangeOrderStatusByAdmin),
                (ConsoleKey.F9, "Manage users", UserController.ShowAllUsers),
                (ConsoleKey.F10, "User roles", UserController.ShowAllUserRoles),
                (ConsoleKey.F11, "Order states", ShopController.ShowAllOrderStates),
            };
        return array;
    }
}
