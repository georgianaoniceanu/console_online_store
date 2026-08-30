namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

public class ManufacturerModel : AbstractModel
{
    public ManufacturerModel(int id, string name)
        : base(id)
    {
        this.Name = name;
    }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"Id:{this.Id} {this.Name}";
    }
}
