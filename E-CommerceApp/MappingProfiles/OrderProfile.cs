using Application.Orders.Commands;
using AutoMapper;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Entities;
using E_CommerceApp.Contracts.Order.Request;
using E_CommerceApp.Contracts.Order.Response;

namespace E_CommerceApp.MappingProfiles;

public sealed class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDetails, OrderResponse>();
        CreateMap<OrderRequest, CreateOrderCommand>();
        CreateMap<OrderItem, OrderItemResponse>();
        CreateMap<OrderItemRequest, AddOrderItemCommand>();
        CreateMap<OrderItemRequest, UpdateQuantityOrderItemCommand>();
    }
}