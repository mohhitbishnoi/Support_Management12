using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<Result<string>>
{
    public int Id { get; set; }

    public CreateUserCommand CreateUser { get; set; }

    public UpdateUserCommand(int id, CreateUserCommand createUser)
    {
        Id = id;
        CreateUser = createUser;
    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            
            var customer = await _unitOfWork.Repository<User>()
                .Entities
                .FirstOrDefaultAsync(x =>
                    x.Id == request.Id &&
                    x.RoleId == (int)RoleId.Customer &&
                    !x.IsDeleted,
                    cancellationToken);

            if (customer == null)
            {
                return Result<string>.BadRequest("Customer not found.");
            }
            
            var emailExists = await _unitOfWork.Repository<User>()
                .Entities
                .AnyAsync(x =>
                    x.Email == request.CreateUser.Email &&
                    x.Id != request.Id &&
                    !x.IsDeleted,
                    cancellationToken);

            if (emailExists)
            {
                return Result<string>.BadRequest("Email already exists.");
            }
            customer.FirstName = request.CreateUser.FirstName;
            customer.LastName = request.CreateUser.LastName;
            customer.Email = request.CreateUser.Email;
            customer.PhoneNumber = request.CreateUser.PhoneNumber;
            customer.Password = request.CreateUser.Password;
            customer.RoleId = (int)RoleId.Customer;

            await _unitOfWork.Repository<User>().PutAsync(customer.Id, customer);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Customer updated successfully.");
        }
    }
}