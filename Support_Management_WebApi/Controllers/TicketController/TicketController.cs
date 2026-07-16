using Application.Features.Tickets.TicketCommands;
using Application.Features.Tickets.TicketQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.TicketController
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTicketCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Customer")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllTicketQueries());
            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Customer")]
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetByIdTicketQueries
            {
                TicketId = id
            });

            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-ticket/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromForm] UpdateTicketCommand command)
        {
            command.TicketId = id;

            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("update-ticket-status")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateTicketStatusCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var response = await _mediator.Send(new DeleteTicketCommand(id));
            return ResponseHelper.GenerateResponse(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMyTickets(int userId)
        {
            var response = await _mediator.Send(new GetMyTicketsQueries
            {
                UserId = userId
            });

            return ResponseHelper.GenerateResponse(response);
        }
    }
}