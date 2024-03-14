using Application.Enums;
using Application.Models;
using Application.Products.Queries;
using Domain.Abstractions;
using Domain.ProductAggregate;
using MediatR;

namespace Application.Products.QueryHandlers;

public class GetProductImagesHandler : IRequestHandler<GetProductImagesQuery, OperationResult<List<ProductImages>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<List<ProductImages>> _result = new();

    public GetProductImagesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<ProductImages>>> Handle(GetProductImagesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<ProductImages> images = await _unitOfWork
                .ProductRepository
                .GetProductImages(request.ProductId);

            _result.Payload = images;
            return _result;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, "Product Images Not Found");
        }

        return _result;
    }
}