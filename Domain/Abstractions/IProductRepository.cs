using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;

namespace Domain.Abstractions;

public interface IProductRepository
{
    Task<Product> GetProductDetailsById(Guid productId);
    Task<List<Product>> GetProducts();
    Task<List<Product>> GetProductsByCategoryName(string categoryName);
    Task CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task<Product?> GetProductCommentsByProductId(Guid productId);
    Task<Product?> GetProductRatesByProductId(Guid productId);
    Task<ProductComment?> GetProductCommentById(Guid productCommentId, Product product);
    Task<ProductRate?> GetProductRateById(Guid productRateId, Product product);
    Task<List<ProductImages>> GetProductImages(Guid productId);
}