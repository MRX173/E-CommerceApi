using Application.Models;
using Application.ProductCategories.Queries;
using Domain.Abstractions;
using MediatR;

namespace Application.ProductCategories.QueryHandlers;

public class GetProductCategoryIdByNameHandler : IRequestHandler<GetProductCategoryIdByNameQuery, OperationResult<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<Guid> _result = new();

    public GetProductCategoryIdByNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Guid>> Handle(GetProductCategoryIdByNameQuery request,
        CancellationToken cancellationToken)
    {
        _result.Payload = await _unitOfWork
            .ProductCategoryRepository
            .GetProductCategoryIdByName(request.CategoryName);
        return _result;
    }
}