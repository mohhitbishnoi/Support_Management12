using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Companies.CompanyQueries;

public class GetAllCompaniesQuery : IRequest<Result<List<Company>>>
{
    internal class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<Company>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _unitOfWork.Repository<Company>().GetAllAsync();

            if (companies == null || !companies.Any())
            {
                return Result<List<Company>>.BadRequest("No companies found.");
            }

            return Result<List<Company>>.Success(companies,"List Of All Registered Companies");
        }
    }
}