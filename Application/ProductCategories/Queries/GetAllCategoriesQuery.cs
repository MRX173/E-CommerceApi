using Application.Models;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.Queries;

public class GetAllCategoriesQuery : IRequest<OperationResult<List<ProductCategory>>>
{
    
}