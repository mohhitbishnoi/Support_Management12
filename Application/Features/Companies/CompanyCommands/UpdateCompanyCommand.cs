using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.CompanyCommands;

public class UpdateCompanyCommand : IRequest<Result<string>>
{
    public int CompanyId { get; set; }
    public CreateCompanyCommand createCompany { get; set; }

    public UpdateCompanyCommand(int companyId,CreateCompanyCommand CreateCompany)
    {
        CompanyId = companyId;
        createCompany = CreateCompany;
    }

    internal class UpdateCompanyCommandhandler : IRequestHandler<UpdateCompanyCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandhandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(command.CompanyId);

            _mapper.Map(command.createCompany, company);
            await _unitOfWork.Repository<Company>().PutAsync(command.CompanyId,company);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Company Updated Successfully");
        }
    }
}
