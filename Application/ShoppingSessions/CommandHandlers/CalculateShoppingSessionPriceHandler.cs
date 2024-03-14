using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ShoppingSessions.Commands;
using Domain.Abstractions;
using Domain.Exceptions;
using MediatR;

namespace Application.ShoppingSessions.CommandHandlers;

public class CalculateShoppingSessionPriceHandler : IRequestHandler<CalculateShoppingSessionPriceCommand,
    OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CalculateShoppingSessionPriceHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession?>> Handle(
        CalculateShoppingSessionPriceCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession?>();
        try
        {
            var session = await _unitOfWork
                .ShoppingSessionRepository
                .CalculateShoppingSessionPrice(request.Id);
            if (session != null)
                await _unitOfWork.ShoppingSessionRepository.UpdateShoppingSession(session);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                result.AddError(ErrorCode.DatabaseOperationException
                    , "Shopping session update failed");
            }

            result.Payload = session;
            return result;
        }
        catch (ShoppingSessionNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.ShoppingSessionNotValidException, e.Message));
        }

        return result;
    }
}