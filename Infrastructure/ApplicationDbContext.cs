using System.Reflection;
using Domain.CommonValueObject;
using Domain.OrderAggregate;
using Domain.ProductAggregate;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Domain.OrderAggregate.Entities;
using Domain.ProductAggregate.Entities;
using Domain.ShoppingSessionAggregate;
using Domain.ShoppingSessionAggregate.Entities;
using Domain.UserAggregate.Entities;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<CustumUser,UserRole,Guid>, IDbContext
{
    private IDbContextTransaction _transaction;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductRate> ProductRates { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<ProductCategory> ProductsCategory { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }
    public DbSet<OrderDetails?> OrderDetails { get; set; }
    public DbSet<OrderItem?> OrderItems { get; set; }
    public DbSet<UserPayment?> UserPayments { get; set; }
    public DbSet<ShoppingSession?> ShoppingSessions { get; set; }
    public DbSet<CartItem?> CartItems { get; set; }
    public DbSet<PaymentDetails> PaymentDetails { get; set; }

    public async Task BegainTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction ??= await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Product Value Objects
        builder.Ignore<Price>();
        builder.Ignore<ProductInventory>();
        builder.Ignore<Discount>();
        builder.Ignore<Sku>();
        builder.Ignore<IsActive>();
        // User Value Objects
        builder.Ignore<UserAddress>();
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}