using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Companies.CompanyCommands;

public class UpdateCompanyCommand : IRequest<Result<string>>
{
    public int CompanyId { get; set; }
    public CreateCompanyCommand CreateCompany { get; set; }

    public UpdateCompanyCommand(int companyId, CreateCompanyCommand createCompany)
    {
        CompanyId = companyId;
        CreateCompany = createCompany;
    }

    internal class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>()
                                           .GetByIdAsync(command.CompanyId);

            if (company == null)
            {
                return Result<string>.BadRequest("Company not found.");
            }

            _mapper.Map(command.CreateCompany, company);

            await _unitOfWork.Repository<Company>()
                             .PutAsync(command.CompanyId, company);

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Company updated successfully.");
        }
    }
}