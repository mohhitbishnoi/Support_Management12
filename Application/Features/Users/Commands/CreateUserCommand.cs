using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using Domain.Entitis.Enums;
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
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _unitOfWork.Repository<User>()
                .Entities
                .AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailExists)
            {
                return Result<string>.BadRequest("Email already exists.");
            }

            var user = _mapper.Map<User>(request);

            // Default Customer Role
            user.RoleId = (int)RoleId.Customer;

            await _unitOfWork.Repository<User>().PostAsync(user);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("User Created Successfully.");
        }
    }
}