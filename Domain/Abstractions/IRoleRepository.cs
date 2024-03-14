using Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Abstractions;

public interface IRoleRepository
{
    Task<UserRole?> GetRoleByName(string roleName);
    Task<UserRole?> GetRoleById(Guid roleId);
    Task<List<UserRole>> GetRoles();
    Task CreateRole(UserRole role);
    Task DeleteRole(Guid roleId);
    Task UpdateRole(UserRole role);
}