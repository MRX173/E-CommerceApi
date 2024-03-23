using Application.Enums;
using Application.Models;
using Application.Products.Queries;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.QueryHandlers;

public class GetProductsByCategoryIdHandler :
    IRequestHandler<GetProductsByCategoryIdQuery, OperationResult<List<Product>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByCategoryIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<Product>>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        OperationResult<List<Product>> result = new OperationResult<List<Product>>();
        try
        {
            List<Product?> products = await _unitOfWork
                .ProductRepository
                .GetProductsByCategoryId(request.CategoryId);
            if (products is null)
            {
                result.AddError(ErrorCode.NotFound, "Products Not Found");
                return result;
            }
            result.Payload = products;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError, e.Message);
        }
        return result;
    }
}