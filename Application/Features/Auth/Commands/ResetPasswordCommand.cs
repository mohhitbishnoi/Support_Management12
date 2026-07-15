using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class ResetPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public int OtpCode { get; set; }
    public string NewPassword { get; set; }

    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>()
                 .Entities.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (user == null)
            {
                return Result<string>.BadRequest("Email or UserName Are not match");
            }


            var isvalidateotp = await _unitOfWork.Repository<Otp>()
                .Entities.AnyAsync(x => x.UserId == user.Id && x.OtpCode == request.OtpCode, cancellationToken);

            if (!isvalidateotp)
            {
                return Result<string>.BadRequest("Invalid OTP");
            }

            user.Password = request.NewPassword;


            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0)
            {
                return Result<string>.BadRequest("Failed to update password");
            }

            return Result<string>.Success("Password changed successfully");
        }
    }
}


