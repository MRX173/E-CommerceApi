using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.Products.Commands;
using Domain.Abstractions;
using Domain.ProductAggregate.Entities;
using MediatR;

namespace Application.Products.CommandHandlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<bool> _result = new();

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Product? product = await _unitOfWork.ProductRepository.GetProductDetailsById(request.Id);
            if (product is null)
            {
                _result.AddError(ErrorCode.NotFound, "Product not found");
                return _result;
            }

            _unitOfWork.ProductRepository.DeleteProduct(product);
            
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbDeleteException e)
            {
                e.ValidationErrors
                    .ForEach(x => _result
                        .AddError(ErrorCode.DatabaseOperationException,
                            $"Product Delete Failed for product ID: {request.Id}"));
            }

            _result.Payload = true;
            return _result;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, "Product Cannot be deleted");
        }

        return _result;
    }
}