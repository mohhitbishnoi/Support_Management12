using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands;

public class VerifyOtpCommand : IRequest<Result<string>>
{
    public int OtpCode { get; set; }
    internal class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyOtpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Repository<Otp>()
               .Entities
               .Where(x => x.OtpCode == request.OtpCode)
               .OrderByDescending(x => x.Time)
               .FirstOrDefaultAsync(cancellationToken);

            if (otp == null)
            {
                return Result<string>.BadRequest("Invalid OTP");
            }

            if (otp.IsUsed)
            {
                return Result<string>.BadRequest("OTP already used");
            }


            if (otp.Time.AddMinutes(10)<=DateTime.UtcNow)
            {
                return Result<string>.BadRequest("OTP has expired");
            }

            var todayOtpCount = await _unitOfWork.Repository<Otp>()
                .Entities
                .Where(x =>
                    x.UserId == otp.UserId &&
                    x.Time.Date == DateTime.UtcNow.Date)
                .CountAsync(cancellationToken);

            if (todayOtpCount < 10)
            {
                return Result<string>.BadRequest("You have reached your daily limit");
            }

            otp.IsUsed = true;

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("OTP verified successfully");
        }
    }
}
       



