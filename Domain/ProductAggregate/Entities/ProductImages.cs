using Domain.Common;
using Domain.ProductAggregate.Entities;

namespace Domain.ProductAggregate;

public class ProductImages : BaseAuditableEntity
{
    private ProductImages()
    {
    }

    public string ImageUrl { get; private set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public void UpdateImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}