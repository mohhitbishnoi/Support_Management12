using Application.Interfaces.Repository;
using Application.Interfaces.Services.MailServices;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class ChangePasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _emailService;

        public ChangePasswordCommandHandler(IMailService emailService, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(ChangePasswordCommand request , CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Email == request.Email);
            if(user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }
            if(user.Password != request.Password)
            {
                return Result<string>.BadRequest($"{user.Password} is invalid.");
            }
            if(request.NewPassword != request.ConfirmPassword)
            {
                return Result<string>.BadRequest("Confirm Password Dose Not Match With New Password");
            }

            return Result<string>.Success("Password Changed Successfullt");
        }
    }
}
