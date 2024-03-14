using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustumUser?> GetUserById(Guid? userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<List<CustumUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CreateUser(CustumUser user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateUser(CustumUser custumUser)
    {
        _context.Users.Update(custumUser);
        return Task.CompletedTask;
    }

    public Task DeleteUser(CustumUser? custumUser)
    {
        _context.Users.Remove(custumUser);
        return Task.CompletedTask;
    }
}