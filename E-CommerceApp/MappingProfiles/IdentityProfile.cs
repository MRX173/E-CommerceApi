using Application.Identity.Commands;
using Application.Identity.DTOs;
using AutoMapper;
using Domain.UserAggregate.Entities;
using E_CommerceApp.Contracts.Identity;

namespace E_CommerceApp.MappingProfiles;

public sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<RegisterCreate, RegisterUserCommand>();
        CreateMap<IdentityUserDto, IdentityResponse>();
        CreateMap<CustumUser, IdentityUserDto>();
        CreateMap<LoginRequest, LoginUserCommand>();
        CreateMap<UpdateRequest, UpdateUserCommand>();
    }
}