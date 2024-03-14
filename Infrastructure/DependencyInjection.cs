using Domain.Abstractions;
using Infrastructure.Abstraction;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPaymentRepository, UserPaymentRepository>();
        services.AddScoped<IShoppingSessionRepository, ShoppingSessionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}