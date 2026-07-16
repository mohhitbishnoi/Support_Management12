using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Commands;

public class DeleteSLASettingCommand : IRequest<Result<string>>
{
    public int Id { get; set; }

    public DeleteSLASettingCommand(int id)
    {
        Id = id;
    }

    internal class DeleteSLASettingCommandHandler
        : IRequestHandler<DeleteSLASettingCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSLASettingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(
            DeleteSLASettingCommand request,
            CancellationToken cancellationToken)
        {
            var sla = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (sla == null)
            {
                return Result<string>.BadRequest("SLA Setting not found.");
            }

            await _unitOfWork.Repository<SlASetting>()
                .DeleteAsync(sla.Id);

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("SLA Setting deleted successfully.");
        }
    }
}