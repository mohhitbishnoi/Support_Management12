using Application.Dtos.Users;
using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<Result<GetUserDto>>
{
    public int Id { get; set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetUserDto>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<User>()
            .Entities
            .Include(x => x.Role)
            .Where(x => x.Id == request.Id
                     && x.RoleId == (int)RoleId.Customer
                     && !x.IsDeleted)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (customer == null)
        {
            return Result<GetUserDto>.BadRequest("Customer not found.");
        }

        return Result<GetUserDto>.Success(customer, "Customer Details");
    }
}