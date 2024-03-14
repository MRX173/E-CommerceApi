using System.Security.Claims;
using Application.Enums;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Abstractions;
using Domain.Exceptions.UserExceptions;
using Domain.UserAggregate.Entities;
using Domain.UserAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Identity.CommandHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly UserManager<CustumUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtService _jwtService;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;

    public RegisterUserHandler(UserManager<CustumUser> userManager, IUnitOfWork unitOfWork, JwtService jwtService,
        IMapper mapper)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            CustumUser? isExisting = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (isExisting != null)
                _result.AddError(ErrorCode.UserAlreadyExists, "Email already exists");

            CustumUser user = await CreateUserIdentity(request);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _result.Payload = _mapper.Map<IdentityUserDto>(user);
            _result.Payload.FirstName = user.FirstName;
            _result.Payload.LastName = user.LastName;
            _result.Payload.Token = GetJwtToken(user);
            return _result;
        }
        catch (UserNotValidException e)
        {
            e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.ValidationError, x));
        }

        return _result;
    }

    private async Task<CustumUser> CreateUserIdentity(RegisterUserCommand request)
    {
        UserAddress userAddress = UserAddress.CreateUserAddress(request.AddressLine, request.City, request.Country);
        CustumUser user = CustumUser.CreateUser(request.FirstName, request.LastName,
            userAddress, request.EmailAddress, request.PhoneNumber);
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(x =>
                _result.AddError(ErrorCode.IdentityCreationFailed, x.Description));
        }
        return user;
    }

    private string GetJwtToken(CustumUser user)
    {
        ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName + " " + user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        });
        string token = _jwtService.GenerateToken(claims);
        return token;
    }
}