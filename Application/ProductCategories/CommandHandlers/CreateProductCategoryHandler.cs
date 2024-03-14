using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ProductCategories.Commands;
using Domain.Abstractions;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate;
using MediatR;

namespace Application.ProductCategories.CommandHandlers;

public class
    CreateProductCategoryHandler : IRequestHandler<CreateProductCategoryCommand, OperationResult<ProductCategory>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<ProductCategory> _result = new();

    public CreateProductCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductCategory>> Handle(CreateProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            ProductCategory productCategory = ProductCategory
                .CreateProductCategory(request.CategoryName,
                    request.CategoryDescription, request.CategoryImageUrl);
            await _unitOfWork.ProductCategoryRepository.CreateProductCategory(productCategory);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors
                    .ForEach(error => _result
                        .AddError(ErrorCode.ProductCategoryCreationNotPossible,
                            "Product Category Creation Failed"));
            }

            _result.Payload = productCategory;
            return _result;
        }
        catch (ProductCategoryNotValidException e)
        {
            e.ValidationErrors
                .ForEach(error => _result
                    .AddError(ErrorCode.ProductCategoryNotValid, e.Message));
        }

        return _result;
    }
}