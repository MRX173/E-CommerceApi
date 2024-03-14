using Application.Models;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.Queries;

public class GetProductCategoryIdByNameQuery : IRequest<OperationResult<Guid>>
{
    public string CategoryName { get; set; } = string.Empty;
}