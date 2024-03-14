using Application.Models;
using Application.ProductCategories.Queries;
using Domain.Abstractions;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.QueryHandlers;

public class GetAllCategoriesHandler :
    IRequestHandler<GetAllCategoriesQuery, OperationResult<List<ProductCategory>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<List<ProductCategory>> _result = new();

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<ProductCategory>>> Handle(GetAllCategoriesQuery request
        , CancellationToken cancellationToken)
    {
        _result.Payload = await _unitOfWork
            .ProductCategoryRepository
            .GetAllCategories();
        return _result;
    }
}