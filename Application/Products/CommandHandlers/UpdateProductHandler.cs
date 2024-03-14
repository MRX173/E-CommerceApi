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

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<Product> _result = new();

    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Product>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            Product product = await GetProduct(request);

            _unitOfWork.ProductRepository.UpdateProduct(product);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                e.ValidationErrors
                    .ForEach(x => _result
                        .AddError(ErrorCode.DatabaseOperationException, "UpdateProduct Failed"));
            }

            _result.Payload = product;
            return _result;
        }
        catch (ProductNotValidException e)
        {
            e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.ProductNotValid, e.Message));
        }

        return _result;
    }

    private async Task<Product> GetProduct(UpdateProductCommand request)
    {
        Product product = await _unitOfWork
            .ProductRepository
            .GetProductDetailsById(request.Id);
        _result = CheckProduct(product);
        Price productToUpdatePrice = Price.Create(request.Price, request.Currency);
        ProductInventory updatedProductInventory = ProductInventory.CreateProductInventory(request.Stock);
        Product productToUpdate = product.UpdateProduct(request.Name, request.Description
            , request.MainImage,
            updatedProductInventory, productToUpdatePrice);
        return productToUpdate;
    }

    private OperationResult<Product> CheckProduct(Product product)
    {
        if (product is null)
        {
            _result.AddError(ErrorCode.NotFound, "Product not found");
            return _result;
        }

        return _result;
    }
}