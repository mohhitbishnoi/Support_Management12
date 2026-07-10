using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using Application.Interfaces.Repository;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entitis.Enums;
using MediatR;
using Shared;

namespace Application.Features.Companies.Commands;

public class UpdatecompanyCommand:IRequest<Result<string>>
{
    public UpdatecompanyCommand(int id, CreateCompanyCommand createCompany)
{
Id = id;
createCompany = createCompany;
}
public int Id { get; set; }
public CreateCompanyCommand CreateCompany { get; set; }
}
internal class UpdateCompanyCommandHandler : IRequestHandler<UpdatecompanyCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<string>> Handle(UpdatecompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Repository<Company>().GetByIdAsync(request.Id);


        //country.Name = request.CreateCountry.Name;
        //country.Code = request.CreateCountry.Code;


        _mapper.Map(request.CreateCompany, company);
        await _unitOfWork.Repository<Company>().PutAsync(request.Id, company);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Company Updated successfully");

    }
}



