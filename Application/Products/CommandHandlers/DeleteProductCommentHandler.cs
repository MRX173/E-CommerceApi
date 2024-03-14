using Application.Enums;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.CommandHandlers;

public class DeleteProductCommentHandler : IRequestHandler<DeleteProductCommentCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteProductCommentCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            var product = await _unitOfWork
                .ProductRepository
                .GetProductCommentsByProductId(request.ProductId);
            if (product != null)
            {
                var comment = await _unitOfWork
                    .ProductRepository
                    .GetProductCommentById(request.ProductCommentId, product);
                if (comment?.CustumUserId != request.UserId)
                {
                    result.AddError(ErrorCode.ProductCommentNotValid,"ProductComment not valid");// TODO error code
                    return result;
                }
            }

            result.Payload = true;
            return result;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError,"ProductComment not valid");
        }
        return result;
    }
}