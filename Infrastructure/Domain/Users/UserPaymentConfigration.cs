using Domain.Enums;
using Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Domain.Users;

public class UserPaymentConfigration : IEntityTypeConfiguration<UserPayment>
{
    public void Configure(EntityTypeBuilder<UserPayment> builder)
    {
        builder.Property(x => x.PaymentType)
            .HasColumnName("PaymentType")
            .HasConversion(new EnumToStringConverter<PaymentType>());
        builder.Property(x => x.Provider)
            .HasColumnName("Provider")
            .HasConversion(new EnumToStringConverter<Provider>());
        builder
            .HasOne<CustumUser>(x=>x.CustumUser)
            .WithMany(x => x.PaymentMethods)
            .HasForeignKey(x => x.CustumUserId);
    }
}