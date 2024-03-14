using Application.Enums;
using Application.Models;
using Application.Products.Queries;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.QueryHandlers;

public class GetProductsByCategoryNameHandler
    : IRequestHandler<GetProductsByCategoryNameQuery, OperationResult<List<Product>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public OperationResult<List<Product>> _result = new();

    public GetProductsByCategoryNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<Product>>> Handle(GetProductsByCategoryNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<Product> products = await _unitOfWork
                .ProductRepository
                .GetProductsByCategoryName(request.CategoryName);

            _result.Payload = products;
            return _result;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, "Products Not Found");
        }
        return _result;
    }
}