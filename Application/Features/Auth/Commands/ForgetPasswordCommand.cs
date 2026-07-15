using Application.Dtos.MailDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Services.MailServices;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class ForgetPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }

    internal class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _emailService;

        public ForgetPasswordCommandHandler(IMailService emailService, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }

            var random = new Random();
            int otpCode = random.Next(1000, 9999);

            var otp = new Otp
            {
                OtpCode = otpCode,
                Time = DateTime.UtcNow,
                UserId = user.Id,
                IsUsed = false
            };

            await _unitOfWork.Repository<Otp>().PostAsync(otp);
            await _unitOfWork.Save(cancellationToken);


            //string Subject = "Forgot-Password OTP Verification";
            //string Body = ("Hi this Side Otps service Handler,We Send you Otp for Forgot-Password" + otp);

            var maildto = new MailDto
            {
                To = request.Email,
                Subject = "Forgot-Password OTP Verification",
                Body = ("<h1>Hi <br> this Side Otps service Handler,<br> We Send you Otp for Forgot-Password </h1>" + otp.OtpCode)
            };

            await _emailService.SendMail(maildto);
            return Result<string>.Success("OTP Sent Successfully");
        }
    }
}



