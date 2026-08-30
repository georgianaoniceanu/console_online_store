namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CategoryModel : AbstractModel
{
    public CategoryModel(int id, string name)
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
