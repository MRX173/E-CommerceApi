using Domain.UserAggregate.Entities;

namespace Domain.Abstractions;

public interface IUserPaymentRepository
{
    Task<UserPayment?> GetUserPaymentById(Guid userPaymentId);
    Task<List<UserPayment?>> GetUserPaymentsByUserId(Guid userId);
    Task CreateUserPayment(UserPayment? userPayment);
    Task DeleteUserPayment(UserPayment? userPayment);
}