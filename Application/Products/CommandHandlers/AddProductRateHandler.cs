using Application.Enums;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.CommandHandlers;

public class AddProductRateHandler : IRequestHandler<AddProductRateCommand, OperationResult<ProductRate>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductRateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductRate>> Handle(AddProductRateCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<ProductRate>();
        try
        {
            var product = await _unitOfWork
                .ProductRepository
                .GetProductDetailsById(request.ProductId);
            var rate = ProductRate
                .CreateProductRate(request.RateValue, request.ProductId, request.UserId);
            product.AddProductRate(rate);
            _unitOfWork.ProductRepository.UpdateProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            result.Payload = rate;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError,"Product Update Failed ");
        }
        return result;
    }
}