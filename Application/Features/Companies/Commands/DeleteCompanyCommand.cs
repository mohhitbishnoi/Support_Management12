using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using Application.Interfaces.Repository;
using Domain.Entitis.Enums;
using MediatR;
using Shared;

namespace Application.Features.Companies.Commands;

public class DeleteCompanyCommand : IRequest<Result<string>>
{
    public int Id { get; set; }

    public DeleteCompanyCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Company>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Company delete successfully");
    }
}



