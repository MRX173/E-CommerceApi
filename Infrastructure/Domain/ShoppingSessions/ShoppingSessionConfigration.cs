using Domain.ShoppingSessionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.ShoppingSessions;

public class ShoppingSessionConfigration : IEntityTypeConfiguration<ShoppingSession>
{
    public void Configure(EntityTypeBuilder<ShoppingSession> builder)
    {
        builder.OwnsOne(x => x.Total);
        builder.HasMany(x => x.CartItems)
            .WithOne(x => x.ShoppingSession)
            .HasForeignKey(x => x.ShoppingSessionId);
    }
}