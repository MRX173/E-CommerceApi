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
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<CustumUser> _userManager;
    private readonly JwtService _jwtService;
    private readonly UserServices _userServices;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;

    public RegisterUserHandler(IUnitOfWork unitOfWork, JwtService jwtService,
        IMapper mapper, UserServices userServices, UserManager<CustumUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _userServices = userServices;
        _userManager = userManager;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var isExisting = await _userServices.GetUserByEmail(request.EmailAddress);
            if (isExisting != null)
            {
                _result.AddError(ErrorCode.UserAlreadyExists, "Email already exists");
                return _result;
            }

            CustumUser? user = await CreateUserIdentity(request);
            var roleResult = await _userServices.AddUserToRole(user, Role.Admin.ToString());
            if (!roleResult.Succeeded)
            {
                roleResult.Errors.ToList().ForEach(x =>
                    _result.AddError(ErrorCode.IdentityCreationFailed, x.Description));
            }

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

    private async Task<CustumUser?> CreateUserIdentity(RegisterUserCommand request)
    {
        UserAddress userAddress = UserAddress.CreateUserAddress(request.AddressLine, request.City, request.Country);
        CustumUser? user = CustumUser.CreateUser(request.FirstName, request.LastName,
            userAddress, request.EmailAddress, request.PhoneNumber);
        IdentityResult result = await _userServices.CreateUser(user, request.Password);
        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(x =>
                _result.AddError(ErrorCode.IdentityCreationFailed, x.Description));
        }

        return user;
    }

    private string GetJwtToken(CustumUser? user)
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