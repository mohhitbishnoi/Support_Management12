using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.CompanyQuries;

public class GetCompanyByIdQuries : IRequest<Result<Company>>
{
    public int CompanyId { get; set; }

    public GetCompanyByIdQuries(int companyid)
    {
        CompanyId = companyid;
    }

    internal class GetCompanyByIdQurieshandler : IRequestHandler<GetCompanyByIdQuries, Result<Company>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompanyByIdQurieshandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Company>> Handle(GetCompanyByIdQuries request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(request.CompanyId);

            if(company == null)
            {
                return Result<Company>.BadRequest("Company Not Found");
            }

            return Result<Company>.Success(company, "Company Found");
        }
    }
}
