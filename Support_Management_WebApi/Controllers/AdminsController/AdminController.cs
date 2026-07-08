using Application.Features.Roles_Access.Admin;
using Application.Features.Roles_Access.Admin.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.AdminsComtroller;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;
    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
   
    [HttpPost("Admin-Register")]
    public async Task<IActionResult>AdminRegisteration(RegisterAdminCommand command)
    {
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpPost("Admin-Login")]
    public async Task<IActionResult>AdminLogin(AdminLoginCommand command)
    {
        var result = await _mediator.Send(command);

        return ResponseHelper.GenerateResponse(result);
    }
    [HttpPost("Add-Employee")]
    public async Task<IActionResult>AddEmployee(AddEmployeeCommand cmd)
    {
        var res =  await _mediator.Send(cmd);

        return ResponseHelper.GenerateResponse(res);

    }
    [HttpPut("Update-Employee")]
    public async Task<IActionResult>UpdateEmployee (UpdateEmployeeCommand cmd)
    {
        var result = await _mediator.Send(cmd);
            return ResponseHelper.GenerateResponse(result);
    }
    [HttpDelete("Delete-Employee")]
    public async Task<IActionResult>DeleteEmployee(DeleteEmployeeCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpGet("View-Employee")]
    public async Task<IActionResult>GetEmployee(ViewAllEmployeeQuery query)
    {
        var result = await _mediator.Send(query);
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpGet("View-Tickets")]
    public async Task<IActionResult>GetAllTickets(ViewAllTicketsQuery query)
    {
        var result = await _mediator.Send(query);
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpGet("View-Users")]
    public async Task<IActionResult>GetAllUsers(ViewUsersQuery query)
    {
        var result = await _mediator.Send(query);
        return ResponseHelper.GenerateResponse(result);
    }
}
