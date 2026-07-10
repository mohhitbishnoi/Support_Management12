using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.CompanyCommands;

public class CreateCompanyCommand : IRequest<Result<string>>,ICreateMapFrom<Company>
{
    public string CompanyName { get; set; }
    public string CompanyLogo { get; set; }

    internal class CreateCompanyCommandhandler :IRequestHandler<CreateCompanyCommand,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompanyCommandhandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(CreateCompanyCommand command,CancellationToken cancellationToken)
        {
            //var company = await _unitOfWork.Repository<Company>().Entities.FirstOrDefaultAsync(x=>x.Id == command.CompanyId);
            
            //if (company != null)
            //{
            //    return Result<string>.BadRequest("Company Already Exist");
            //}

            //var newcompany = new Company
            //{
            //    Id = command.CompanyId,
            //    CompanyName = command.CompanyName,
            //    CompanyLogo = command.CompanyLogo
            //};
                
            var newcompany = _mapper.Map<Company>(command);
            await _unitOfWork.Repository<Company>().PostAsync(newcompany);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Company Created Successfully");

        }
    }
}
