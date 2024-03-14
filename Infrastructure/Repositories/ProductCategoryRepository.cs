using Domain.Abstractions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<ProductCategory>> GetAllCategories()
    {
        return await _dbContext.ProductsCategory.ToListAsync();
    }

    public async Task<Guid> GetProductCategoryIdByName(string categoryName)
    {
        return await _dbContext
            .ProductsCategory
            .Where(x => x.Name == categoryName)
            .Select(x => x.Id).FirstOrDefaultAsync();
    }

    public async Task<ProductCategory> GetProductCategoryById(Guid id)
    {
        return await _dbContext
            .ProductsCategory
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateProductCategory(ProductCategory category)
    {
        await _dbContext.ProductsCategory.AddAsync(category);
    }

    public void UpdateProductCategory(ProductCategory category)
    {
        _dbContext.ProductsCategory.Update(category);
    }

    public void DeleteProductCategory(ProductCategory category)
    {
        _dbContext.ProductsCategory.Remove(category);
    }
}