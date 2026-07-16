using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Queries;

public class GetSLAByPriorityQuery : IRequest<Result<SlASetting>>
{
    public TicketPriority Priority { get; set; }

    public GetSLAByPriorityQuery(TicketPriority priority)
    {
        Priority = priority;
    }

    internal class GetSLAByPriorityQueryHandler
        : IRequestHandler<GetSLAByPriorityQuery, Result<SlASetting>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSLAByPriorityQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SlASetting>> Handle(
            GetSLAByPriorityQuery request,
            CancellationToken cancellationToken)
        {
            var slaSetting = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Priority == request.Priority, cancellationToken);

            if (slaSetting == null)
            {
                return Result<SlASetting>.BadRequest("SLA Setting not found for selected priority.");
            }

            return Result<SlASetting>.Success(slaSetting,"");
        }
    }
}