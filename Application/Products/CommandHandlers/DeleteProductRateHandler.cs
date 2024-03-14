using Application.Enums;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using MediatR;

namespace Application.Products.CommandHandlers;

public class DeleteProductRateHandler : IRequestHandler<DeleteProductRateCommand,OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductRateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteProductRateCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            var product = await _unitOfWork
                .ProductRepository
                .GetProductRatesByProductId(request.ProductId);
            if (product == null)
            {
                result.AddError(ErrorCode.NotFound, "Product not found"); // TODO: error code
                return result;
            }

            var rate = await _unitOfWork
                .ProductRepository
                .GetProductRateById(request.RateId, product);
            if (rate == null)
            {
                result.AddError(ErrorCode.NotFound, "Product rate not found"); // TODO: error code
                return result;
            }

            if (request.UserId != rate.CustumUserId)
            {
                result.AddError(ErrorCode.ServerError, "User id mismatch with rate"); // TODO: error code
                return result;
            }
            product.AddProductRate(rate);
            _unitOfWork.ProductRepository.UpdateProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            result.Payload = true;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError,"Unable to handle delete product rate command");
        }
        return result;
    }
}