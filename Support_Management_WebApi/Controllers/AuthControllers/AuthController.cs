using Application.Features.Users.Commands;
using Application.Interfaces.Repository;
using MediatR;
using Domain.Entitis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.AuthController;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(response);
    }

    [HttpPost("Login")]

    public async Task<IActionResult> Login(UserLoginCommand command)
    {
        var response = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(response);
    }

    [HttpPost("Forget-password")]

    public async Task<IActionResult> Forgetpassword(ForgetPasswordCommand forget)
    {
        var response = await _mediator.Send(forget);
        return ResponseHelper.GenerateResponse(response);
    }

    [HttpPost("Verify-Otp")]

    public async Task<IActionResult> Verifyotp(VerifyOtpCommand command)
    {
        var response = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(response);
    }

    [HttpPost("Reset-password")]

    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(response);
    }


    [HttpPost("Change-password")]

    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(response);
    }
}
