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
    [Authorize(Roles ="Customer")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-ticket")]
        public async Task<IActionResult> Create([FromForm] CreateTicketCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("all-tickets")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllTicketQueries());
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetByIdTicketQueries
            {
                TicketId = id
            });

            return ResponseHelper.GenerateResponse(response);
        }

        [HttpPut("update-ticket/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromForm] UpdateTicketCommand command)
        {
            command.TicketId = id;

            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("update-ticket-status")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateTicketStatusCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpDelete("delete-ticket/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var response = await _mediator.Send(new DeleteTicketCommand(id));
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("my-tickets/{userId}")]
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