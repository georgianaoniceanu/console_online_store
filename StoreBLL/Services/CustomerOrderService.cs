namespace StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
public class CustomerOrderService : ICrud
{
    // Order state ids, matching TestDataFactory.GetOrderStateData()
    private const int NewOrder = 1;
    private const int CancelledByUser = 2;
    private const int CancelledByAdministrator = 3;
    private const int Confirmed = 4;
    private const int MovedToDeliveryCompany = 5;
    private const int InDelivery = 6;
    private const int DeliveredToClient = 7;
    private const int DeliveryConfirmedByClient = 8;

    // Allowed transitions, according to Documentation/OrderStatusChanges.jpg
    private static readonly Dictionary<int, int[]> AllowedTransitions = new ()
    {
        [NewOrder] = new[] { Confirmed },
        [Confirmed] = new[] { CancelledByUser, CancelledByAdministrator, MovedToDeliveryCompany },
        [MovedToDeliveryCompany] = new[] { CancelledByAdministrator, InDelivery },
        [InDelivery] = new[] { CancelledByAdministrator, DeliveredToClient },
        [DeliveredToClient] = new[] { CancelledByUser, DeliveryConfirmedByClient },
        [CancelledByUser] = Array.Empty<int>(),
        [CancelledByAdministrator] = Array.Empty<int>(),
        [DeliveryConfirmedByClient] = Array.Empty<int>(),
    };

    private readonly ICustomerOrderRepository repository;

    public CustomerOrderService(ICustomerOrderRepository repository)
    {
        this.repository = repository;
    }

    public void Add(AbstractModel model)
    {
        var x = (CustomerOrderModel)model;

        // Business rule: every newly created order starts as "New Order",
        // regardless of what the caller sent.
        x.OrderStateId = NewOrder;

        this.repository.Add(new CustomerOrder(x.Id, x.OperationTime, x.UserId, x.OrderStateId));
    }

    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll()
            .Select(x => new CustomerOrderModel(x.Id, x.OperationTime, x.UserId, x.OrderStateId));
    }

    public IEnumerable<AbstractModel> GetByUserId(int userId)
    {
        return this.repository.GetByUserId(userId)
            .Select(x => new CustomerOrderModel(x.Id, x.OperationTime, x.UserId, x.OrderStateId));
    }

    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        if (res == null)
        {
            return null;
        }

        return new CustomerOrderModel(res.Id, res.OperationTime, res.UserId, res.OrderStateId);
    }

    public void Update(AbstractModel model)
    {
        var x = (CustomerOrderModel)model;
        var existing = this.repository.GetById(x.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Comanda nu a fost gasita.");
        }

        this.ChangeState(existing, x.OrderStateId);
    }

    /// <summary>
    /// Called by a registered user, cancels their own order (only allowed from certain states).
    /// </summary>
    public void CancelByUser(int orderId)
    {
        var existing = this.repository.GetById(orderId);
        if (existing == null)
        {
            throw new InvalidOperationException("Comanda nu a fost gasita.");
        }

        this.ChangeState(existing, CancelledByUser);
    }

    /// <summary>
    /// Called by a registered user, confirms that the order was received.
    /// </summary>
    public void ConfirmDeliveryByClient(int orderId)
    {
        var existing = this.repository.GetById(orderId);
        if (existing == null)
        {
            throw new InvalidOperationException("Comanda nu a fost gasita.");
        }

        this.ChangeState(existing, DeliveryConfirmedByClient);
    }

    /// <summary>
    /// Called by an administrator to move an order to any valid next state.
    /// </summary>
    public void ChangeStateByAdmin(int orderId, int newStateId)
    {
        var existing = this.repository.GetById(orderId);
        if (existing == null)
        {
            throw new InvalidOperationException("Comanda nu a fost gasita.");
        }

        this.ChangeState(existing, newStateId);
    }

    private void ChangeState(CustomerOrder existing, int newStateId)
    {
        var currentState = existing.OrderStateId;

        if (!AllowedTransitions.TryGetValue(currentState, out var nextStates) || !nextStates.Contains(newStateId))
        {
            // Invalid transition requested: keep the last valid state, as required by the task.
            throw new InvalidOperationException(
                $"Tranzitie de status invalida: din starea {currentState} nu se poate trece in starea {newStateId}.");
        }

        existing.OrderStateId = newStateId;
        this.repository.Update(existing);
    }
}
