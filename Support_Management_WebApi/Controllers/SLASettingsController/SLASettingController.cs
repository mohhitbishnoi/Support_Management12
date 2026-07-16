using Application.Features.SLASettings.Commands;
using Application.Features.SLASettings.Queries;
using Domain.Entitis.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.SLASettingsController;

[Route("api/[controller]")]
[ApiController]
public class SLASettingController : ControllerBase
{
    
        private readonly IMediator _mediator;

        public SLASettingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create-SLASetting")]
        public async Task<IActionResult> Create(CreateSLASettingCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }
        [HttpPut("Update-SLASetting/{id}")]
        public async Task<IActionResult> Update(int id, CreateSLASettingCommand command)
        {
            var response = await _mediator.Send(
                new UpdateSLASettingCommand(id, command.Priority));

            return ResponseHelper.GenerateResponse(response);
        }
        [HttpDelete("Delete-SLASetting/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(
                new DeleteSLASettingCommand(id));

            return ResponseHelper.GenerateResponse(response);
        }
        [HttpGet("Get-All-SLASettings")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(
                new GetAllSLASettingsQuery());

            return ResponseHelper.GenerateResponse(response);
        }
        [HttpGet("Get-SLASetting-ById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(
                new GetSLASettingByIdQuery(id));

            return ResponseHelper.GenerateResponse(response);
        }
        [HttpGet("Get-SLASetting-ByPriority/{priority}")]
        public async Task<IActionResult> GetByPriority(TicketPriority priority)
        {
            var response = await _mediator.Send(
                new GetSLAByPriorityQuery(priority));

            return ResponseHelper.GenerateResponse(response);
        }
}
