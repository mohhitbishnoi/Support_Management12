using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin;

public class UpdateEmployeeCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public long PhoneNumber { get; set; }

    public UpdateEmployeeCommand(int id)
    {
        Id = id;
    }

    internal class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateEmployeeCommand command,CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);

            if (employee == null)
            {
                return Result<string>.BadRequest("Employee not found");
            }
            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Email = command.Email;
            employee.PhoneNumber = command.PhoneNumber;

            await _unitOfWork.Repository<User>().PutAsync(command.Id, employee);

            await _unitOfWork.Save(cancellationToken);
            return Result<string>.Success("Employee Updated Successfully");
        }
    }


}

