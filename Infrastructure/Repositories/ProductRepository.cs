using Domain.Abstractions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductDetailsById(Guid productId)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId)!;
    }

    public async Task<List<Product>> GetProductsByCategoryId(Guid categoryId)
    {
        return await _dbContext
            .Products
            .Where(x => x.ProductCategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _dbContext
            .Products
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryName(string categoryName)
    {
        return await _dbContext
            .Products
            .Include(x => x.Category)
            .Where(x => x.Category.Name == categoryName)
            .ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _dbContext
            .Products
            .AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        _dbContext
            .Products
            .Update(product);
    }

    public void DeleteProduct(Product product)
    {
        _dbContext
            .Products
            .Remove(product);
    }

    public async Task<Product?> GetProductCommentsByProductId(Guid productId)
    {
        return await _dbContext
            .Products
            .Include(x => x.ProductComments)
            .FirstOrDefaultAsync(x => x.Id == productId);
    }
    public async Task<Product?> GetProductRatesByProductId(Guid productId)
    {
        return await _dbContext
            .Products
            .Include(x => x.ProductRate)
            .FirstOrDefaultAsync(x => x.Id == productId);
    }
    public async Task<ProductComment?> GetProductCommentById(Guid productCommentId, Product product)
    {
        var productComment = product
            .ProductComments
            .FirstOrDefault(x => x != null && x.Id == productCommentId);
        return await Task.FromResult(productComment);
    }

    public async Task<ProductRate?> GetProductRateById(Guid productRateId, Product product)
    {
        var productRate = product
            .ProductRate
            .FirstOrDefault(x => x.Id == productRateId);
        return await Task.FromResult(productRate);
    }

    public async Task<List<ProductImages>> GetProductImages(Guid productId)
    {
        return await _dbContext
            .ProductImages
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }
}