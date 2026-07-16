using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Queries;

public class GetAllSLASettingsQuery : IRequest<Result<List<SlASetting>>>
{
    internal class GetAllSLASettingsQueryHandler
        : IRequestHandler<GetAllSLASettingsQuery, Result<List<SlASetting>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSLASettingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<SlASetting>>> Handle(
            GetAllSLASettingsQuery request,
            CancellationToken cancellationToken)
        {
            var slaSettings = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            if (slaSettings == null || !slaSettings.Any())
            {
                return Result<List<SlASetting>>.BadRequest("No SLA Settings found.");
            }

            return Result<List<SlASetting>>.Success(slaSettings,"");
        }
    }
}