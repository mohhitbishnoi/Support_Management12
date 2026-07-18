using Application.Dtos.MailDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Services.MailServices;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using NETCore.MailKit.Core;
using Shared;

namespace Application.Features.Roles_Access.Admin.Commands;

public class AssignEmployeeCommand : IRequest<Result<string>>
{
    public int TicketId { get; set; }
    public int EmployeeId { get; set; }
}

internal class AssignEmployeeCommandHandler
    : IRequestHandler<AssignEmployeeCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;

    public AssignEmployeeCommandHandler(
        IUnitOfWork unitOfWork,
        IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _mailService = mailService;
    }

    public async Task<Result<string>> Handle(
        AssignEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Repository<Ticket>()
            .GetByIdAsync(command.TicketId);

        if (ticket == null)
            return Result<string>.BadRequest("Ticket not found.");

        var employee = await _unitOfWork.Repository<User>()
            .GetByIdAsync(command.EmployeeId);

        if (employee == null)
            return Result<string>.BadRequest("Employee not found.");

        if (employee.RoleId != (int)RoleId.Employee)
            return Result<string>.BadRequest("Selected user is not an employee.");

        if (ticket.AssignedEmployeeId == employee.Id)
            return Result<string>.BadRequest("This ticket is already assigned to this employee.");

        ticket.AssignedEmployeeId = employee.Id;

        await _unitOfWork.Save(cancellationToken);

        string subject = "New Ticket Assigned";

        string body = $@"
            <h2>New Ticket Assigned</h2>

            <p>Hello {employee.FirstName},</p>

            <p>A new support ticket has been assigned to you.</p>

            <table border='1' cellpadding='8'>
                <tr>
                    <td><b>Ticket Id</b></td>
                    <td>{ticket.Id}</td>
                </tr>
                <tr>
                    <td><b>Title</b></td>
                    <td>{ticket.TicketTitle}</td>
                </tr>
                <tr>
                    <td><b>Description</b></td>
                    <td>{ticket.TicketDescription}</td>
                </tr>
            </table>

            <br/>

            <p>Please login to the Support Management System to start working on this ticket.</p>

            <br/>

            <b>Support Management Team</b>";

        var mail = new MailDto {
            To = employee.Email,
            Subject=subject,
            Body=body};
        await _mailService.SendMail(mail);

        return Result<string>.Success("Employee assigned successfully.");
    }
}