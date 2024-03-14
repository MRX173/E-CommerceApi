using Domain.Abstractions;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IUserPaymentRepository UserPaymentRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IShoppingSessionRepository ShoppingSessionRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }

    private bool _disposed;

    public UnitOfWork(ApplicationDbContext dbContext, IUserRepository userRepository,
        IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, IShoppingSessionRepository shoppingSessionRepository, IUserPaymentRepository userPaymentRepository, IOrderRepository orderRepository, IRoleRepository roleRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        ProductCategoryRepository = productCategoryRepository;
        ProductRepository = productRepository;
        ShoppingSessionRepository = shoppingSessionRepository;
        UserPaymentRepository = userPaymentRepository;
        OrderRepository = orderRepository;
        RoleRepository = roleRepository;
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _dbContext.Dispose();
        _disposed = true;
    }
}