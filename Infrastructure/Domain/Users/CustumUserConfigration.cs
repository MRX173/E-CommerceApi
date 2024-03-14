using Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.Users;

public class CustumUserConfigration : IEntityTypeConfiguration<CustumUser>
{
    public void Configure(EntityTypeBuilder<CustumUser> builder)
    {
        builder.OwnsOne(x => x.UserAddress);

        builder.HasMany(x => x.ProductRate)
            .WithOne(x => x.CustumUser)
            .HasForeignKey(x => x.CustumUserId);

        builder.HasMany(x => x.ProductComments)
            .WithOne(x => x.CustumUser)
            .HasForeignKey(x => x.CustumUserId);

        builder.HasMany(x => x.ShoppingSessions)
            .WithOne(x => x.CustumUser)
            .HasForeignKey(x => x.CustumUserId);

        builder.HasMany(x => x.PaymentMethods)
            .WithOne(x => x.CustumUser)
            .HasForeignKey(x => x.CustumUserId);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.CustumUser)
            .HasForeignKey(x => x.CustumUserId);
    }
}