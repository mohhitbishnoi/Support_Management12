using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Tickets.TicketCommands;

public class DeleteTicketCommand : IRequest<Result<string>>
{
    public int TicketId { get; set; }
    public DeleteTicketCommand(int ticketid)
    {
        TicketId = ticketid;
    }
}


public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(request.TicketId);

        if (ticket == null)
        {
            return  Result<string>.BadRequest("Ticket not found.");
        }

        await _unitOfWork.Repository<Ticket>().DeleteAsync(request.TicketId);

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Ticket deleted successfully.");
    }
}