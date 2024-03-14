using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Application.ShoppingSessions.Commands;
using Domain.Abstractions;
using Domain.CommonValueObject;
using Domain.Exceptions;
using MediatR;

namespace Application.ShoppingSessions.CommandHandlers;

public class CreateShoppingSessionHandler : IRequestHandler<CreateShoppingSessionCommand,
    OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateShoppingSessionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession?>> Handle(
        CreateShoppingSessionCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Domain.ShoppingSessionAggregate.Entities.ShoppingSession?>();
        try
        {
           // var price = Price.Create(request.Amount, request.Currency);
            var shoppingSession = Domain.ShoppingSessionAggregate.Entities
                .ShoppingSession
                .CreateShoppingSession(request.UserId);
            await _unitOfWork.ShoppingSessionRepository.CreateShoppingSession(shoppingSession);
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DbCreateException e)
            {
                e.ValidationErrors
                    .ForEach(x => result
                        .AddError(ErrorCode.DatabaseOperationException
                            , "Could not create shopping session."));
            }

            result.Payload = shoppingSession;
            return result;
        }
        catch (ShoppingSessionNotValidException e)
        {
            e.ValidationErrors
                .ForEach(x => result
                    .AddError(ErrorCode.ShoppingSessionNotValidException
                        , e.Message));
        }

        return result;
    }
}