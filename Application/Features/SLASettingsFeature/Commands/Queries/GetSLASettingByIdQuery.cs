using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SLASettings.Queries;

public class GetSLASettingByIdQuery : IRequest<Result<SlASetting>>
{
    public int Id { get; set; }

    public GetSLASettingByIdQuery(int id)
    {
        Id = id;
    }

    internal class GetSLASettingByIdQueryHandler
        : IRequestHandler<GetSLASettingByIdQuery, Result<SlASetting>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSLASettingByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SlASetting>> Handle(
            GetSLASettingByIdQuery request,
            CancellationToken cancellationToken)
        {
            var slaSetting = await _unitOfWork.Repository<SlASetting>()
                .Entities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (slaSetting == null)
            {
                return Result<SlASetting>.BadRequest("SLA Setting not found.");
            }

            return Result<SlASetting>.Success(slaSetting,"");
        }
    }
}