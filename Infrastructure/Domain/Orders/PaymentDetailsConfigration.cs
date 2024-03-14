using Domain.Enums;
using Domain.OrderAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Domain.Orders;

public class PaymentDetailsConfigration : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.OwnsOne(x => x.Amount);
        builder.Property(x => x.Provider)
            .HasColumnName("Provider")
            .HasConversion(new EnumToStringConverter<Provider>());

        builder.Property(x => x.PaymentStatus)
            .HasColumnName("PaymentStatus")
            .HasConversion(new EnumToStringConverter<Status>());
    }
}