using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using Domain.Entitis;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin;

public class AddEmployeeCommand:IRequest<Result<string>>,ICreateMapFrom<User>
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public long PhoneNumber { get; set; }

    public string Password { get; set; }

    internal class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(AddEmployeeCommand command, CancellationToken cancellationToken)
        {
            var emailExists = await _unitOfWork.Repository<User>()
                .Entities
                .AnyAsync(x => x.Email == command.Email, cancellationToken);

            if (emailExists)
                return Result<string>.BadRequest("Email already exists.");

            var employee = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Password = command.Password,

                RoleId = (int)RoleId.Employee
            };

            await _unitOfWork.Repository<User>().PostAsync(employee);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Employee Created Successfully");
        }
    }
}


