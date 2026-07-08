using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin;

public class DeleteEmployeeCommand:IRequest<Result<string>>,ICreateMapFrom<User>
{
    public int Id { get; set; }

    public DeleteEmployeeCommand(int id)
    {
        Id = id;
    }
    internal class DeleteEmployeeCommandhandler : IRequestHandler<DeleteEmployeeCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandhandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>>Handle(DeleteEmployeeCommand command,CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            if(employee == null)
            {
                return Result<string>.BadRequest("Employee Not Found");
            }
            await _unitOfWork.Repository<User>().DeleteAsync(command.Id);
            await _unitOfWork.Save(cancellationToken);
            return Result<string>.Success("Employee Deleted Successfully");
        }
    }
}

