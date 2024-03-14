using Domain.OrderAggregate;
using Domain.OrderAggregate.Entities;

namespace Domain.Abstractions;

public interface IOrderRepository
{
    Task<List<OrderDetails>> GetAllOrdersByUserId(Guid userId);
    Task<OrderDetails?> CalculateOrderPrice(Guid orderDetailsId); 
    Task<OrderItem?> GetOrderItemById(Guid orderItemId);
    Task<OrderDetails?> GetOrderDetailsById(Guid orderId);
    Task CreateOrder(OrderDetails? order);
    Task AddOrderItem(OrderItem orderItem);
    Task UpdateQuantityOrderItem(OrderItem? orderItem);
    Task DeleteOrderItem(OrderItem? orderItem);
}