using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long PhoneNumber { get; set; }
    public string Password { get; set; }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _unitOfWork.Repository<User>()
                .Entities
                .AnyAsync(x => x.Email == request.Email && !x.IsDeleted, cancellationToken);

            if (emailExists)
            {
                return Result<string>.BadRequest("Email already exists.");
            }
            var customer = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                RoleId = (int)RoleId.Customer 
            };

            await _unitOfWork.Repository<User>().PostAsync(customer);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Customer Registered Successfully.");
        }
    }
}