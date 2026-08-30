namespace StoreDAL.Data.InitDataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

public class TestDataFactory : AbstractDataFactory
{
    public override Category[] GetCategoryData()
    {
        return new[]
        {
            new Category(1, "fruits"),
            new Category(2, "water"),
            new Category(3, "vegetables"),
            new Category(4, "seafood"),
            new Category(5, "meet"),
            new Category(6, "grocery"),
            new Category(7, "milk food"),
            new Category(8, "smartphones"),
            new Category(9, "laptop"),
            new Category(10, "photocameras"),
            new Category(11, "kitchen accesories"),
            new Category(12, "spices"),
            new Category(13, "Juice"),
            new Category(14, "alcohol drinks"),
        };
    }

    public override CustomerOrder[] GetCustomerOrderData()
    {
        // CustomerOrder(id, operationTime, userId, orderStateId)
        // userId refers to User.Id above, orderStateId refers to OrderState.Id below.
        return new[]
        {
            new CustomerOrder(1, "2026-08-01 10:15:00", 2, 1), // Mary's order, "New Order"
            new CustomerOrder(2, "2026-08-10 14:30:00", 2, 4), // Mary's order, "Confirmed"
        };
    }

    public override Manufacturer[] GetManufacturerData()
    {
        return new[]
        {
            new Manufacturer(1, "Samsung"),
            new Manufacturer(2, "Apple"),
            new Manufacturer(3, "Lenovo"),
            new Manufacturer(4, "Canon"),
            new Manufacturer(5, "Nestle"),
            new Manufacturer(6, "Coca-Cola"),
        };
    }

    public override OrderDetail[] GetOrderDetailData()
    {
        // OrderDetail(id, orderId, productId, price, amount)
        // orderId refers to CustomerOrder.Id, productId refers to Product.Id defined below.
        return new[]
        {
            new OrderDetail(1, 1, 1, 799.99m, 1),
            new OrderDetail(2, 1, 6, 1.20m, 2),
            new OrderDetail(3, 2, 3, 1299.00m, 1),
        };
    }

    public override OrderState[] GetOrderStateData()
    {
        return new[]
        {
            new OrderState(1, "New Order"),
            new OrderState(2, "Cancelled by user"),
            new OrderState(3, "Cancelled by administrator"),
            new OrderState(4, "Confirmed"),
            new OrderState(5, "Moved to delivery company"),
            new OrderState(6, "In delivery"),
            new OrderState(7, "Delivered to client"),
            new OrderState(8, "Delivery confirmed by client"),
        };
    }

    public override Product[] GetProductData()
    {
        // Product(id, titleId, manufacturerId, description, price)
        // titleId refers to ProductTitle.Id, manufacturerId refers to Manufacturer.Id above.
        return new[]
        {
            new Product(1, 1, 1, "Samsung Galaxy S23, 128GB, Black", 799.99m),
            new Product(2, 2, 2, "iPhone 15, 128GB, Blue", 899.00m),
            new Product(3, 3, 3, "Lenovo ThinkPad X1 Carbon, 16GB RAM", 1299.00m),
            new Product(4, 4, 4, "Canon EOS R50 Mirrorless Camera", 749.50m),
            new Product(5, 5, 5, "Bananas, 1kg", 1.50m),
            new Product(6, 6, 6, "Mineral Water 1.5L", 1.20m),
        };
    }

    public override ProductTitle[] GetProductTitleData()
    {
        // ProductTitle(id, title, categoryId)
        // categoryId refers to Category.Id defined in GetCategoryData().
        return new[]
        {
            new ProductTitle(1, "Samsung Galaxy S23", 8),   // 8 = smartphones
            new ProductTitle(2, "iPhone 15", 8),             // 8 = smartphones
            new ProductTitle(3, "Lenovo ThinkPad X1 Carbon", 9), // 9 = laptop
            new ProductTitle(4, "Canon EOS R50", 10),        // 10 = photocameras
            new ProductTitle(5, "Banana", 1),                // 1 = fruits
            new ProductTitle(6, "Mineral Water 1.5L", 2),    // 2 = water
        };
    }

    public override User[] GetUserData()
    {
        // User(id, name, lastName, login, password, roleId)
        // roleId refers to UserRole.Id (1 = Admin, 2 = Registered, 3 = Guest).
        // Passwords below are SHA256 hashes (base64) so they match UserService.HashPassword.
        // Plain-text login credentials for testing: admin/admin123, mary/mary123
        return new[]
        {
            new User(1, "John", "Admin", "admin", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", 1),
            new User(2, "Mary", "Smith", "mary", "YSCsdEkHyqYvI2vSaVtQR4x/3xf/+5KKyEUHrTMLK+g=", 2),
        };
    }

    public override UserRole[] GetUserRoleData()
    {
        return new[]
        {
            new UserRole(1, "Admin"),
            new UserRole(2, "Registered"),
            new UserRole(3, "Guest"),
        };
    }
}
