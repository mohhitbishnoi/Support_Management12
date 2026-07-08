using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands;

public class UserLoginCommand:IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }

}
internal class UserLoginCommandHandler:IRequestHandler<UserLoginCommand,Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserLoginCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().Entities.Where(x=>x.Email == request.Email && x.Password == request.Password).FirstOrDefaultAsync();
        return Result<string>.Success("Login SuccessFull");
    }
}

    
    

