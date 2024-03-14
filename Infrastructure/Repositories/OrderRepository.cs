using Domain.Abstractions;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDetails>> GetAllOrdersByUserId(Guid userId)
    {
        return await _context.OrderDetails
            .Include(x => x.CustumUser)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .Where(x => x.CustumUser.Id == userId)
            .ToListAsync();
    }

    public async Task<OrderDetails?> CalculateOrderPrice(Guid orderDetailsId)
    {
        var orderDetails = await GetOrderDetailsById(orderDetailsId);
        if (orderDetails != null)
        {
            foreach (var orderItem in orderDetails.OrderItems)
            {
                orderDetails.TotalPrice.Value += orderItem.Product.Price.Value * orderItem.Quantity;
                orderDetails.TotalPrice.Currency = orderItem.Product.Price.Currency;
            }
        }

        return orderDetails;
    }

    public async Task<OrderItem?> GetOrderItemById(Guid orderItemId)
    {
        return await _context
            .OrderItems
            .FirstOrDefaultAsync(x => x.Id == orderItemId);
    }

    public async Task<OrderDetails?> GetOrderDetailsById(Guid orderId)
    {
        return await _context.OrderDetails
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task CreateOrder(OrderDetails? order)
    {
        await _context.OrderDetails.AddAsync(order);
    }


    public async Task AddOrderItem(OrderItem orderItem)
    {
        var orderDetails = await _context.OrderDetails
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == orderItem.OrderDetailsId);
        if (orderDetails != null)
        {
            orderDetails.AddOrderItemToOrderDetails(orderItem);
        }

        await _context.OrderItems.AddAsync(orderItem);
    }

    public Task UpdateQuantityOrderItem(OrderItem? orderItem)
    {
        _context.OrderItems.Update(orderItem);
        return Task.CompletedTask;
    }

    public Task DeleteOrderItem(OrderItem? orderItem)
    {
        _context.OrderItems.Remove(orderItem);
        return Task.CompletedTask;
    }
}