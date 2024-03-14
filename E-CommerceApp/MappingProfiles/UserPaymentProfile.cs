using Application.UserPayments.Commands;
using AutoMapper;
using Domain.UserAggregate.Entities;
using E_CommerceApp.Contracts.UserPayments.Request;
using E_CommerceApp.Contracts.UserPayments.Response;

namespace E_CommerceApp.MappingProfiles;

public class UserPaymentProfile : Profile
{
    public UserPaymentProfile()
    {
        CreateMap<UserPayment, UserPaymentResponse>();
        CreateMap<UserPaymentCreate, CreateUserPaymentCommand>()
            .ForMember(des => des.Provider
                , opt =>
                    opt.MapFrom(src => src.Provider.ToString()))
            .ForMember(des => des.PaymentType
                , opt =>
                    opt.MapFrom(src => src.PaymentType.ToString()));
    }
}