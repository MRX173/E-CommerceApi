using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<UserRole> _roleManager;

    public RoleRepository(RoleManager<UserRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<UserRole?> GetRoleByName(string roleName)
    {
        return await _roleManager.FindByNameAsync(roleName);
    }

    public async Task<UserRole?> GetRoleById(Guid roleId)
    {
        return await _roleManager.FindByIdAsync(roleId.ToString());
    }

    public async Task<List<UserRole>> GetRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task CreateRole(UserRole role)
    {
        await _roleManager.CreateAsync(role);
    }

    public async Task DeleteRole(Guid roleId)
    {
        var role = await GetRoleById(roleId);
        if (role != null)
            await _roleManager.DeleteAsync(role);
    }

    public async Task UpdateRole(UserRole role)
    {
        await _roleManager.UpdateAsync(role);
    }
}