using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using Application.Dtos.CompaniesDto;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis.Enums;
using MediatR;
using Shared;

namespace Application.Features.Companies.Queries;

public class GetAllCompanyQueries : IRequest<Result<GetCompanyDto>>
{
}

    internal class GetAllCompanyQueriesHandler : IRequestHandler<GetAllCompanyQueries, Result<List<GetCompanyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCompanyQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<List<GetCompanyDto>>> Handle(GetAllCompanyQueries request, CancellationToken cancellationToken)
    {
        var Companys = await _unitOfWork.Repository<Company>().GetAllAsync();
        var result = _mapper.Map<List<GetCompanyDto>>(Companys);
        return Result<List<GetCompanyDto>>.Success(result, "Compnys");
    }

    }
}