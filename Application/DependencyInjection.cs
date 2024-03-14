using System.Reflection;
using Application.Abstractions;
using Application.Behaviors;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<JwtService>();
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>),
                typeof(TransactionBehavior<,>));
        });
        return services;
    }
}