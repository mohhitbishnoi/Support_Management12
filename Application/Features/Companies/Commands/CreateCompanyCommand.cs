using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;

using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Shared;

namespace Application.Features.Companies.Commands;

public class CreateCompanyCommand : IRequest<Result<int>>, ICreateMapFrom<Company>
{
    public string Name {  get; set; }
    public string logo {  get; set; }
}
internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {

            Name = request.Name,
            logo = request.logo,
        };
        var country = _mapper.Map<Company>(request);

        await _unitOfWork.Repository<Company>().PostAsync(company);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(company.Id, "Created Successfully");
    }

}