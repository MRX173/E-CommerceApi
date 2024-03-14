using Domain.OrderAggregate;
using Domain.OrderAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.Orders;

public class OrderDetailsConfigration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder
            .HasOne(o => o.PaymentDetails)
            .WithOne(p => p.OrderDetails)
            .HasForeignKey<PaymentDetails>(pi => pi.OrderDetailsId);
        builder.HasMany(x=>x.OrderItems)
            .WithOne(x=>x.OrderDetails)
            .HasForeignKey(x=>x.OrderDetailsId);

        builder.OwnsOne(x => x.TotalPrice);
    }
}