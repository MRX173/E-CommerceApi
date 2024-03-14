using Domain.CommonValueObject;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domain.Products;

public class ProductConfigration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasMany(x => x.ProductRate)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
        
        builder.HasMany(x => x.ProductComments)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
        
        builder.HasMany(x => x.ProductImages)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
        
        builder.OwnsOne(x => x.Price);
        builder.OwnsOne(x => x.Discount);
        builder.OwnsOne(x => x.Sku);
        builder.OwnsOne(x => x.Inventory);
    }
}