using Application.Dtos.Users;
using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles_Access.Admin;

public class ViewAllEmployeeQuery : IRequest<Result<List<GetUserDto>>>
{
    internal class ViewAllEmployeeQueryHandler
        : IRequestHandler<ViewAllEmployeeQuery, Result<List<GetUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewAllEmployeeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetUserDto>>> Handle(
            ViewAllEmployeeQuery request,
            CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Repository<User>()
                .Entities
                .Include(x => x.Role)
                .Where(x => x.RoleId == (int)RoleId.Employee && !x.IsDeleted)
                .Select(x => new GetUserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    RoleId = (RoleId)x.RoleId,
                    RoleName = x.Role != null ? x.Role.RoleName : ""
                })
                .ToListAsync(cancellationToken);

            if (!employees.Any())
            {
                return Result<List<GetUserDto>>.BadRequest("Employee not found.");
            }

            return Result<List<GetUserDto>>.Success(employees, "Employee List");
        }
    }
}