using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserServices
{
    private readonly UserManager<CustumUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public UserServices(UserManager<CustumUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustumUser?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<CustumUser?> GetUserById(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<IdentityResult> CreateUser(CustumUser? user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }

    public async Task<bool> CheckPassword(CustumUser? user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> AddUserToRole(CustumUser? user, string roleName)
    {
        if (user != null)
            await _userManager.AddToRoleAsync(user, roleName);
        return IdentityResult.Success;
    }

    public async Task RemoveUserFromRole(CustumUser? user, string roleName)
    {
        await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IList<string>> GetRoles(CustumUser? user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsInRole(CustumUser? user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }
}