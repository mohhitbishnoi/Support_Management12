using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.CompanyCommands;

public class DeleteCompanyCommand : IRequest<Result<string>>
{
    public int CompanyId { get; set; }

    public DeleteCompanyCommand(int companyId)
    {
        CompanyId = companyId;
    }

    internal class DeleteCompanyCommandhandler : IRequestHandler<DeleteCompanyCommand,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompanyCommandhandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(DeleteCompanyCommand command,CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Company>().DeleteAsync(command.CompanyId);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Company Delete Successfully");
        }
    }
}
