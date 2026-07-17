using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin.Commands;

public class AssignEmployeeCommand : IRequest<Result<string>>
{
    public int Ticketid { get; set; }
    public int EmployeeId { get; set; }

    internal class AssignEmployeeCommandhnadler : IRequestHandler<AssignEmployeeCommand,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignEmployeeCommandhnadler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(AssignEmployeeCommand command,CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(command.Ticketid);

            if(ticket == null)
            {
                return Result<string>.BadRequest("Ticket Not Found");
            }

            var employee = await _unitOfWork.Repository<Ticket>().GetByIdAsync(command.EmployeeId);

            if(employee == null)
            {
                return Result<string>.BadRequest("Employee Not Found");
            }

            ticket.AssignedEmployeeId = employee.Id;
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Employee Assigned Successfully");
        }
    }
}
