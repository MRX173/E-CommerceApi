using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.CommonValueObject;
using Domain.Exceptions.ProductExceptions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.CommandHandlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<Product> _result = new();

    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Product>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            Guid categoryId = await _unitOfWork
                .ProductCategoryRepository
                .GetProductCategoryIdByName(request.CategoryName);
            Price productPrice = Price.Create(request.Price, request.Currency);
            Sku productSku = Sku.Create(request.SkuCode);
            ProductInventory productInventory = ProductInventory.CreateProductInventory(request.Stock);
            Product product = Product.CreateProduct(request.Name, request.Description,
                request.MainImage, productPrice,
                productSku, productInventory, categoryId);
            await _unitOfWork.ProductRepository.CreateProduct(product);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.ProductCreationNotPossible,
                    "Product cannot be created"));
            }

            _result.Payload = product;
            return _result;
        }
        catch (ProductNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => _result
                    .AddError(ErrorCode.ProductCreationNotPossible, "Product cannot be created"));
        }

        return _result;
    }
}