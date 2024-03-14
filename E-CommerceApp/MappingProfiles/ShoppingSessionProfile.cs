using Application.ShoppingSession.Commands;
using AutoMapper;
using Domain.OrderAggregate.Entities;
using Domain.ShoppingSessionAggregate.Entities;
using E_CommerceApp.Contracts.ShoppingSessions.Request;
using E_CommerceApp.Contracts.ShoppingSessions.Response;

namespace E_CommerceApp.MappingProfiles;

public class ShoppingSessionProfile : Profile
{
    public ShoppingSessionProfile()
    {
        CreateMap<ShoppingSession, ShoppingSessionResponse>()
            .ForMember(des => des.Value
                , opt => opt
                    .MapFrom(src => src.Total.Value))
            .ForMember(des => des.Currency
                , opt => opt
                    .MapFrom(src => src.Total.Currency));
        CreateMap<CartItem, CartItemResponse>();
        CreateMap<AddItemRequest, AddItemToShoppingSessionCommand>();
    }
}