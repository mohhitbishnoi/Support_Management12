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
            var otp = await _unitOfWork.Repository<Otp>().Entities.OrderByDescending(x => x.Time).FirstOrDefaultAsync(y=>y.OtpCode == request.OtpCode);
            if (otp == null)
            {
                return Result<string>.BadRequest("Invalid OTP code.");
            }
            if(otp.IsUsed)
            {
                return Result<string>.BadRequest("OTP code has already been used.");
            }
            var TodayOtpCount = await _unitOfWork.Repository<Otp>().Entities.Where
                (x=>x.Id==otp.Id && x.Time.Date == DateTime.Now.Date).CountAsync();
           if(TodayOtpCount <= 10)
            {
                return Result<string>.BadRequest("You have exceeded the maximum number of OTP requests for today.");
            }
           otp.IsUsed = true;
            return Result<string>.Success("OTP verified successfully.");
        }
    }
}
       



