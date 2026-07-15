using Application.Interfaces.Repository;
using Domain.Entitis;
using MediatR;
using Shared;

namespace Application.Features.Tickets.TicketQueries;

public class GetByIdTicketQueries : IRequest<Result<Ticket>>
{
    public int TicketId { get; set; }

    internal class GetByIdTicketQueriesHandler : IRequestHandler<GetByIdTicketQueries, Result<Ticket>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByIdTicketQueriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Ticket>> Handle(GetByIdTicketQueries request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(request.TicketId);

            if (ticket == null)
            {
                return Result<Ticket>.BadRequest("Ticket not found.");
            }

            return Result<Ticket>.Success(ticket, "Ticket fetched successfully.");
        }
    }
}