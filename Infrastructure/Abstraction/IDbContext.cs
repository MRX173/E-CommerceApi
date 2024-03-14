namespace Infrastructure.Abstraction;

public interface IDbContext
{
    Task BegainTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task RetryOnExceptionAsync(Func<Task> func);
}