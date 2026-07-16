using Application.Interfaces.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entitis;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class ResetPasswordCommand : IRequest<Result<string>>
{

    public string NewPassword { get; set; }

    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResetPasswordCommandHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Result<string>.BadRequest("Invalid token.");
            }

            int userId = int.Parse(userIdClaim);

            var user = await _unitOfWork.Repository<User>()
                .Entities
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user == null)
            {
                return Result<string>.BadRequest("User not found.");
            }

            user.Password = request.NewPassword; // Hash before saving

            await _unitOfWork.Repository<User>().PutAsync(user.Id, user);

            var result = await _unitOfWork.Save(cancellationToken);

            if (result <= 0)
            {
                return Result<string>.BadRequest("Failed to update password.");
            }

            return Result<string>.Success("Password changed successfully.");
        }
    }
}


