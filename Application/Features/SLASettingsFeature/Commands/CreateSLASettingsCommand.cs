using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Commands;

public class CreateSLASettingCommand : IRequest<Result<string>>, ICreateMapFrom<SlASetting>
{
    public TicketPriority Priority { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSLASettingCommand, SlASetting>();
    }

    internal class CreateSLASettingCommandHandler
        : IRequestHandler<CreateSLASettingCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSLASettingCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(
            CreateSLASettingCommand request,
            CancellationToken cancellationToken)
        {
            // Check duplicate priority
            var exists = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .AnyAsync(x => x.Priority == request.Priority, cancellationToken);

            if (exists)
            {
                return Result<string>.BadRequest("SLA Setting already exists for this priority.");
            }

            var sla = _mapper.Map<SlASetting>(request);
            //sla.Priority = request.Priority;

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
                .PostAsync(sla);

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("SLA Setting created successfully.");
        }
    }
}