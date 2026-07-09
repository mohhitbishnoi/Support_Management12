using Application.Dtos.Users;
using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Queries;

public class GetAllUsersQuery : IRequest<Result<List<GetUserDto>>>
{
}

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetUserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetUserDto>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<User>()
            .Entities
            .Include(x => x.Role)
            .Where(x => x.RoleId == (int)RoleId.Customer && !x.IsDeleted)
            .Select(x => new GetUserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RoleId = (RoleId)x.RoleId,
                RoleName = x.Role != null ? x.Role.RoleName : string.Empty
            })
            .ToListAsync(cancellationToken);

        if (!customers.Any())
        {
            return Result<List<GetUserDto>>.BadRequest("Customer not found.");
        }

        return Result<List<GetUserDto>>.Success(customers, "Customer List");
    }
}