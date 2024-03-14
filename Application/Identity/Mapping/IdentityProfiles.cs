using Application.Identity.DTOs;
using AutoMapper;
using Domain.UserAggregate.Entities;

namespace Application.Identity.Mapping;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<CustumUser, IdentityUserDto>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.EmailAddress,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.AddressLine,
                opt => opt.MapFrom(src => src.UserAddress.AddressLine))
            .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.UserAddress.City))
            .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.UserAddress.Country));
    }
}