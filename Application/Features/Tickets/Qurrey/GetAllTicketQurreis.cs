using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Tickets.TicketQueries;

public class GetAllTicketQueries : IRequest<Result<List<Ticket>>>
{
    internal class GetAllTicketQueriesHandler : IRequestHandler<GetAllTicketQueries, Result<List<Ticket>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Ticket>>> Handle(GetAllTicketQueries request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync();

            if (tickets == null || !tickets.Any())
            {
                return Result<List<Ticket>>.BadRequest("No tickets found.");
            }

            return Result<List<Ticket>>.Success(tickets, "List Of All Tickets");
        }
    }
}