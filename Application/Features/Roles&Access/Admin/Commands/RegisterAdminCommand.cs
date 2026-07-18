using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles_Access.Admin.Commands;

public class RegisterAdminCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public long PhoneNumber { get; set; }

    internal class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public RegisterAdminCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Result<string>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
        {
            var emailExists = await _unitOfWork.Repository<User>().Entities

        .AnyAsync(x => x.Email == command.Email && !x.IsDeleted, cancellationToken);


            if (emailExists)
            {
                return Result<string>.BadRequest("Email already exists.");

            }
            var admin = new User
            {
                FirstName = command.FirstName,

                LastName = command.LastName,

                Email = command.Email,

                Password = command.Password,

                PhoneNumber = command.PhoneNumber,

                RoleId = (int)RoleId.Admin

            };


            await _unitOfWork.Repository<User>().PostAsync(admin);

            await _unitOfWork.Save(cancellationToken);


            return Result<string>.Success("Admin Registered Successfully.");
        }
    }
}