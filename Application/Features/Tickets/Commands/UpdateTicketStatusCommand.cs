using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Tickets.TicketCommands;

public class UpdateTicketStatusCommand : IRequest<Result<int>>
{
    public int TicketId { get; set; }
    public string Status { get; set; }
}

public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Repository<Ticket>()
            .GetByIdAsync(request.TicketId);

        if (ticket == null)
        {
            return Result<int>.BadRequest("Ticket not found.");
        }

        ticket.Status = request.Status;

        await _unitOfWork.Repository<Ticket>()
            .PutAsync(ticket.Id, ticket);

        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(ticket.Id, "Ticket status updated successfully.");
    }
}