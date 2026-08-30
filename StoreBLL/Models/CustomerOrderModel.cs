namespace StoreBLL.Models;

public class CustomerOrderModel : AbstractModel
{
    public CustomerOrderModel(int id, string operationTime, int userId, int orderStateId)
        : base(id)
    {
        this.OperationTime = operationTime;
        this.UserId = userId;
        this.OrderStateId = orderStateId;
    }

    public int UserId { get; set; }

    public string OperationTime { get; set; }

    public int OrderStateId { get; set; }

    public override string ToString()
    {
        return $"Id:{this.Id} {this.OperationTime} (state:{this.OrderStateId})";
    }
}
