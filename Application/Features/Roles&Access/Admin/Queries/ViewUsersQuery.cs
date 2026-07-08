using Application.Dtos.Users;
using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin
{
    public class ViewUsersQuery : IRequest<Result<List<GetUserDto>>>
    {
        internal class ViewUsersQueryHandler
            : IRequestHandler<ViewUsersQuery, Result<List<GetUserDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ViewUsersQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<GetUserDto>>> Handle(
                ViewUsersQuery request,
                CancellationToken cancellationToken)
            {
                var users = await _unitOfWork
                    .Repository<User>()
                    .Entities
                    .Include(x => x.Role)
                    .Where(x => !x.IsDeleted)
                    .Select(x => new GetUserDto
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        RoleName = x.Role != null ? x.Role.RoleName : ""
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<GetUserDto>>.Success(users,"");
            }
        }
    }
}
