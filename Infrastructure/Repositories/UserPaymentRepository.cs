using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserPaymentRepository : IUserPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserPaymentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserPayment?> GetUserPaymentById(Guid userPaymentId)
    {
        return await _dbContext
            .UserPayments
            .FirstOrDefaultAsync(x => x.Id == userPaymentId);
    }

    public async Task<List<UserPayment?>> GetUserPaymentsByUserId(Guid userId)
    {
        return await _dbContext
            .UserPayments
            .Where(x => x.CustumUserId == userId)
            .ToListAsync();
    }

    public async Task CreateUserPayment(UserPayment? userPayment)
    {
        await _dbContext.UserPayments.AddAsync(userPayment);
    }


    public Task DeleteUserPayment(UserPayment? userPayment)
    {
        _dbContext.UserPayments.Remove(userPayment);
        return Task.CompletedTask;
    }
}