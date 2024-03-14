using Domain.UserAggregate.Entities;

namespace Domain.Abstractions;

public interface IUserRepository
{
    Task<CustumUser?> GetUserById(Guid? userId);
    Task<List<CustumUser>> GetAllUsers();
    // Task CreateUser(CustumUser user);
    // Task UpdateUser(CustumUser custumUser);
    Task DeleteUser(CustumUser? custumUser);
}