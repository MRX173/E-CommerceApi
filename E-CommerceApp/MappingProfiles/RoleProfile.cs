using AutoMapper;
using Domain.UserAggregate.Entities;
using E_CommerceApp.Contracts.Roles.Response;

namespace E_CommerceApp.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<UserRole,IdentityRoleResponse>();
    }
}