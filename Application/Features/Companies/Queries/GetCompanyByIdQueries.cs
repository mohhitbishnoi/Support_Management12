using System;
using System.Collections.Generic;

using System.Text;
using Application.Dtos.CompaniesDto;
using Application.Interfaces.Repository;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Features.Companies.Queries;

public class GetCompanyByIdQueries : IRequest<Result<List<GetCompanyDto>>>
{
}
internal class GetAllCompanyQueryHandler : IRequestHandler<GetAllCompanyQuery, Result<List<GetCompanyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<List<GetCountryDto>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        var countries = await _unitOfWork.Repository<Country>().GetAll();
        var result = _mapper.Map<List<GetCountryDto>>(countries);
        return Result<List<GetCountryDto>>.Success(result, "Countries...");
    }

}

