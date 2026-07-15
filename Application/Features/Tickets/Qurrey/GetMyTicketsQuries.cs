using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Tickets.TicketQueries;

public class GetMyTicketsQueries : IRequest<Result<List<Ticket>>>
{
    public int UserId { get; set; }
}

internal class GetMyTicketsQueriesHandler : IRequestHandler<GetMyTicketsQueries, Result<List<Ticket>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyTicketsQueriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<Ticket>>> Handle(GetMyTicketsQueries request, CancellationToken cancellationToken)
    {
        var tickets = (await _unitOfWork.Repository<Ticket>().GetAllAsync())
            .Where(x => x.Id == request.UserId)
            .ToList();

        if (!tickets.Any())
        {
            return Result<List<Ticket>>.BadRequest("No tickets found.");
        }

        return Result<List<Ticket>>.Success(tickets, "List Of My Tickets");
    }
}