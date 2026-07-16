using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.UsersControllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Users-Registraion")]
    public async Task<IActionResult>CreateUser(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpPut("User-Update")]
    public async Task<IActionResult> UpdateUser(int id, CreateUserCommand command)
    {
        var res = await _mediator.Send(new UpdateUserCommand(id, command));

        return ResponseHelper.GenerateResponse(res);
    }
    [HttpDelete("Delete-User")]
    public async Task<IActionResult>DeleteUser(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return ResponseHelper.GenerateResponse(result);
    }
    [HttpGet("GetUser-id")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var res = await _mediator.Send(new GetUserByIdQuery(id));
        return ResponseHelper.GenerateResponse(res);
    }
    [HttpGet("All Users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new  GetAllUsersQuery());
        return ResponseHelper.GenerateResponse(result);
    }


}
