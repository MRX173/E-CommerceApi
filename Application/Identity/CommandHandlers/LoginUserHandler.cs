using System.Security.Claims;
using Application.Enums;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Abstractions;
using Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Identity.CommandHandlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly UserServices _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtService _jwtService;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly IMapper _mapper;
    
    
    public LoginUserHandler( IUnitOfWork unitOfWork, JwtService jwtService, IMapper mapper, UserServices userService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<OperationResult<IdentityUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CustumUser? user = await _userService.GetUserByEmail(request.EmailAddress);
            if (user == null)
                _result.AddError(ErrorCode.UserDoesNotExist, "User not found");
            bool isPasswordValid = await _userService.CheckPassword(user, request.Password);
            if (!isPasswordValid)
                _result.AddError(ErrorCode.IncorrectPassword, "Wrong password");
            
            _result.Payload = _mapper.Map<IdentityUserDto>(user);
            _result.Payload.EmailAddress = user.Email;
            _result.Payload.Token = GetJwtToken(user);
            return _result;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, e.Message);
        }
        return _result;
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