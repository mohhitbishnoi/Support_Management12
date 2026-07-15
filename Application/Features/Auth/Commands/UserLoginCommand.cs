using Application.Features.Auth.Commands;
using Application.Interfaces.Repository;
using Application.Interfaces.Services.TokenServices;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class UserLoginCommand : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<LoginResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public UserLoginCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>()
            .Entities
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return Result<LoginResponse>.BadRequest("Invalid Email");
        }

        if (user.Password != request.Password)
        {
            return Result<LoginResponse>.BadRequest("Invalid Password");
        }

        var token = await _tokenService.GenerateToken(
            user.Id.ToString(),
            user.Role.RoleName
        );

        var response = new LoginResponse
        {
            Token = token,
            Role = user.Role.RoleName,
            UserName = $"{user.FirstName} {user.LastName}"
        };

        return Result<LoginResponse>.Success(
            response,
            token,
            "Login Successful"
        );
    }
}