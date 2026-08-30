namespace StoreBLL.Models;

public class UserModel : AbstractModel
{
    public UserModel(int id, string name, string lastName, string login, string password, int roleId)
        : base(id)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.RoleId = roleId;
    }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }

    public override string ToString()
    {
        return $"Id:{this.Id} {this.Name} {this.LastName} ({this.Login})";
    }
}
