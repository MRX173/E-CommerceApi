using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ProductCategories.Commands;
using Domain.Abstractions;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.CommandHandlers;

public class DeleteProductCategoryHandler : IRequestHandler<DeleteProductCategoryCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        OperationResult<bool> result = new OperationResult<bool>();
        try
        {
            ProductCategory productCategory = await _unitOfWork
                .ProductCategoryRepository
                .GetProductCategoryById(request.ProductCategoryId);
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(productCategory);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbDeleteException e)
            {
                result.AddError(ErrorCode.ProductCategoryDeletionFailed,
                    $"ProductCategory Deletion Failed ProductCategoryId: {request.ProductCategoryId}");
            }

            result.Payload = true;
            return result;
        }
        catch (ProductCategoryNotValidException e)
        {
            result.AddError(ErrorCode.ProductCategoryNotValid, e.Message);
        }

        return result;
    }
}