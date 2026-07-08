using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateUserCommand createUser { get; set; }
    public UpdateUserCommand(int id, CreateUserCommand createUserCommand)
    {
        Id = id;
        createUser = createUserCommand;
    }
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
           
            //user.FirstName = request.createUser.FirstName;
            //user.LastName = request.createUser.LastName;
            //user.Email = request.createUser.Email;
            //user.PhoneNumber = request.createUser.PhoneNumber;
            //user.Password = request.createUser.Password;

            _mapper.Map(request.createUser,user);
            await _unitOfWork.Repository<User>().PutAsync(request.Id,user);
            await _unitOfWork.Save(cancellationToken);
            return Result<string>.Success("User updated successfully.");
        }
    }
}
