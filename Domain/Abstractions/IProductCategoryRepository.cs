using Domain.ProductAggregate;

namespace Domain.Abstractions;

public interface IProductCategoryRepository 
{
    Task<List<ProductCategory>> GetAllCategories();
    Task<Guid> GetProductCategoryIdByName(string categoryName);
    Task<ProductCategory> GetProductCategoryById(Guid id);
    Task CreateProductCategory(ProductCategory category);
    void UpdateProductCategory(ProductCategory category);
    void DeleteProductCategory(ProductCategory category);
}