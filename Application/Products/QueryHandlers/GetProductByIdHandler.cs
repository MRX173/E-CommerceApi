using Application.Enums;
using Application.Models;
using Application.Products.Queries;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.QueryHandlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    public OperationResult<Product> _result = new();

    public GetProductByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWork
            .ProductRepository
            .GetProductDetailsById(request.ProductId);
        if (product is null)
        {
            _result.AddError(ErrorCode.NotFound, "Product not found");
            return _result;
        }

        _result.Payload = product;
        return _result;
    }
}