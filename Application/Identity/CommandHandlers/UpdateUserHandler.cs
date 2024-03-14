using Application.Abstractions;
using Application.Enums;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using AutoMapper;
using Domain.Abstractions;
using Domain.Exceptions.UserExceptions;
using Domain.UserAggregate.Entities;
using Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Application.Identity.CommandHandlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;


    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            CustumUser? user = await _unitOfWork.UserRepository.GetUserById(request.UserId);
            if (user == null)
                _result.AddError(ErrorCode.UserDoesNotExist, "User not found");
            UserAddress? userAddress = user?.UpdateUserAdderess(request.AddressLine, request.City, request.Country);
            CustumUser? result = user?.UpdateUser(request.FirstName, request.LastName
                , userAddress, request.PhoneNumber);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _result.Payload = _mapper.Map<IdentityUserDto>(user);
            _result.Payload.EmailAddress = user?.Email;
            return _result;
        }
        catch (UserNotValidException e)
        {
            e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.ValidationError, x));
        }

        return _result;
    }
}