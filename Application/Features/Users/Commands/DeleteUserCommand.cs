using Application.Interfaces.Repository;
using Domain.Entitis;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<Result<string>>
{
    public int Id { get; set; }

    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        // Check if Customer exists
        var customer = await _unitOfWork.Repository<User>()
            .Entities
            .FirstOrDefaultAsync(x =>
                x.Id == command.Id &&
                x.RoleId == (int)RoleId.Customer &&
                !x.IsDeleted,
                cancellationToken);

        if (customer == null)
        {
            return Result<string>.BadRequest("Customer not found.");
        }

        await _unitOfWork.Repository<User>().DeleteAsync(customer.Id);

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Customer Deleted Successfully.");
    }
}