using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class ConfigrationExtension
{
    internal static void AddConfiguration(this ModelBuilder modelBuilder)
    {
        List<Type> configrationClasses = 
            Assembly
                .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();
        foreach (Type type in configrationClasses)
        {
            dynamic configuration = Activator.CreateInstance(type);
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}