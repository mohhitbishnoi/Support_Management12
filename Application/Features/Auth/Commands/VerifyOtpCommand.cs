using Application.Interfaces.Repository;
using Application.Interfaces.Services.TokenServices;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class VerifyOtpCommand : IRequest<Result<string>>
{
    public int OtpCode { get; set; }
    public string Email { get; set; }

    internal class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public VerifyOtpCommandHandler(
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>()
                .Entities
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }

            var otp = await _unitOfWork.Repository<Otp>()
                .Entities
                .Where(x => x.UserId == user.Id && x.OtpCode == request.OtpCode)
                .OrderByDescending(x => x.Time)
                .FirstOrDefaultAsync(cancellationToken);

            if (otp == null)
            {
                var otpExistsForOtherUser = await _unitOfWork.Repository<Otp>()
                    .Entities
                    .AnyAsync(x => x.OtpCode == request.OtpCode, cancellationToken);

                if (otpExistsForOtherUser)
                {
                    return Result<string>.BadRequest("Invalid OTP");
                }

                return Result<string>.BadRequest("Invalid OTP");
            }

            if (otp.IsUsed)
            {
                return Result<string>.BadRequest("OTP already used");
            }

            if (otp.Time.AddMinutes(10) <= DateTime.UtcNow)
            {
                return Result<string>.BadRequest("OTP has expired");
            }

            var todayOtpCount = await _unitOfWork.Repository<Otp>()
                .Entities
                .Where(x => x.UserId == user.Id &&
                            x.Time.Date == DateTime.UtcNow.Date)
                .CountAsync(cancellationToken);

            if (todayOtpCount >= 20)
            {
                return Result<string>.BadRequest("You have reached your daily limit");
            }

            otp.IsUsed = true;

            await _unitOfWork.Save(cancellationToken);

           
            string token =await _tokenService.GenerateToken(user.Id,user.Email);

            var result = Result<string>.Success("OTP verified successfully");
            result.Token = token;

            return result;
        }
    }
}