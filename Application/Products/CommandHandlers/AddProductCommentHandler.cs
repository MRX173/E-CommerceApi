using Application.Abstractions;
using Application.Enums;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.CommandHandlers;

public class AddProductCommentHandler : IRequestHandler<AddProductCommentCommand, OperationResult<ProductComment>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductComment>> Handle(AddProductCommentCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<ProductComment>();
        try
        {
            var product = await _unitOfWork.ProductRepository.GetProductDetailsById(request.ProductId);
            var productComment = ProductComment
                .CreateProductComment(request.Text, request.ProductId, request.CustumUserId);
            product.AddProductComments(productComment);
            _unitOfWork.ProductRepository.UpdateProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            result.Payload = productComment;
            return result;
        }
        catch (ProductCommentNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.ProductCommentNotValid
                        , "Product comment is not valid"));
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError, e.Message);
        }

        return result;
    }
}