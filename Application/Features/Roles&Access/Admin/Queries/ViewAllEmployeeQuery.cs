using Application.Dtos.Users;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin;

public class ViewAllEmployeeQuery:IRequest<Result<List<GetUserDto>>>
{
    internal class ViewAllEmployeeQueryHandler : IRequestHandler<ViewAllEmployeeQuery, Result<List<GetUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ViewAllEmployeeQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetUserDto>>> Handle(
            ViewAllEmployeeQuery request,
            CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Repository<User>().Entities.Include(x => x.Role).ToListAsync();
            //   .Select(x => new GetUserDto
            //   {
            //       Id = x.Id,
            //       FirstName = x.FirstName,
            //       LastName = x.LastName,
            //       Email = x.Email,
            //       PhoneNumber = x.PhoneNumber
            //   }).ToListAsync(cancellationToken);

            //return Result<List<GetUserDto>>.Success(employees,"...");

            var data = _mapper.Map<List<GetUserDto>>(employees);
            return Result<List<GetUserDto>>.Success(data, "Employee");
        }

    }
}
