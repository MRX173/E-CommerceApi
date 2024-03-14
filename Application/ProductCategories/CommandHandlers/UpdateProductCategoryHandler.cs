using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ProductCategories.Commands;
using Domain.Abstractions;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.CommandHandlers;

public class UpdateProductCategoryHandler :
    IRequestHandler<UpdateProductCategoryCommand, OperationResult<ProductCategory>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<ProductCategory> _result = new();

    public UpdateProductCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductCategory>> Handle(UpdateProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            ProductCategory productCategory = await _unitOfWork
                .ProductCategoryRepository
                .GetProductCategoryById(request.ProductCategoryId);
            ProductCategory updatedProductCategory = productCategory?
                .UpdateProductCategory(request.CategoryName,
                    request.CategoryDescription, request.CategoryImageUrl);
            if (productCategory is null)
            {
                _result.AddError(ErrorCode.ProductCategoryNotValid, "Product category is not valid");
                return _result;
            }

            _unitOfWork.ProductCategoryRepository.UpdateProductCategory(updatedProductCategory);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                _result.AddError(ErrorCode.DatabaseOperationException
                    , $"Database Cannot Be Updated Product Category Id {productCategory.Id}");
            }

            _result.Payload = productCategory;
            return _result;
        }
        catch (ProductCategoryNotValidException e)
        {
            _result.AddError(ErrorCode.ProductCategoryNotValid, e.Message);
        }

        return _result;
    }
}