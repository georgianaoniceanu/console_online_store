namespace StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

/// <summary>
/// Provides business logic services.
/// </summary>
public class UserService : ICrud
{
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (UserModel)model;

        if (string.IsNullOrWhiteSpace(x.Login))
        {
            throw new ArgumentException("Login-ul nu poate fi gol.");
        }

        if (string.IsNullOrWhiteSpace(x.Password))
        {
            throw new ArgumentException("Parola nu poate fi goala.");
        }

        if (this.repository.GetByLogin(x.Login) != null)
        {
            throw new InvalidOperationException("Login-ul este deja folosit.");
        }

        var hashedPassword = HashPassword(x.Password);
        this.repository.Add(new User(x.Id, x.Name, x.LastName, x.Login, hashedPassword, x.RoleId));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll()
            .Select(x => new UserModel(x.Id, x.Name, x.LastName, x.Login, x.Password, x.RoleId));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        if (res == null)
        {
            return null;
        }

        return new UserModel(res.Id, res.Name, res.LastName, res.Login, res.Password, res.RoleId);
    }

    public UserModel Login(string login, string password)
    {
        var user = this.repository.GetByLogin(login);
        if (user == null)
        {
            return null;
        }

        var hashedPassword = HashPassword(password);
        if (user.Password != hashedPassword)
        {
            return null;
        }

        return new UserModel(user.Id, user.Name, user.LastName, user.Login, user.Password, user.RoleId);
    }

    public void Update(AbstractModel model)
    {
        var x = (UserModel)model;
        var existing = this.repository.GetById(x.Id);
        var passwordToStore = existing != null && existing.Password == x.Password
            ? x.Password
            : HashPassword(x.Password);

        this.repository.Update(new User(x.Id, x.Name, x.LastName, x.Login, passwordToStore, x.RoleId));
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
