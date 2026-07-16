using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Commands;

public class UpdateSLASettingCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public TicketPriority Priority { get; set; }

    public UpdateSLASettingCommand(int id, TicketPriority priority)
    {
        Id = id;
        Priority = priority;
    }

    internal class UpdateSLASettingCommandHandler
        : IRequestHandler<UpdateSLASettingCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSLASettingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateSLASettingCommand request, CancellationToken cancellationToken)
        {
            var sla = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (sla == null)
            {
                return Result<string>.BadRequest("SLA Setting not found.");
            }

            sla.Priority = request.Priority;

            switch (request.Priority)
            {
                case TicketPriority.Critical:
                    sla.ResponseTimeHours = 1;
                    sla.ResolutionTimeHours = 1;
                    break;

                case TicketPriority.High:
                    sla.ResponseTimeHours = 1;
                    sla.ResolutionTimeHours = 2;
                    break;

                case TicketPriority.Medium:
                    sla.ResponseTimeHours = 2;
                    sla.ResolutionTimeHours = 4;
                    break;

                case TicketPriority.Low:
                    sla.ResponseTimeHours = 4;
                    sla.ResolutionTimeHours = 8;
                    break;

                default:
                    return Result<string>.BadRequest("Invalid Priority.");
            }

            await _unitOfWork.Repository<SlASetting>()
                .PutAsync(request.Id, sla);

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("SLA Setting updated successfully.");
        }
    }
}