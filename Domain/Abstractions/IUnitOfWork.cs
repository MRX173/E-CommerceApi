namespace Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{ 
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IUserPaymentRepository UserPaymentRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IShoppingSessionRepository ShoppingSessionRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}