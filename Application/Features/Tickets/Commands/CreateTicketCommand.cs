using Application.Commons.Mappings;
using Application.Dtos.MailDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Services.FilePathService;
using Application.Interfaces.Services.MailServices;
using Application.Interfaces.Services.Users;
using AutoMapper;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Tickets.TicketCommands;

public class CreateTicketCommand : IRequest<Result<int>>, ICreateMapFrom<Ticket>
{
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public TicketPriority ticketPriority { get; set; }
    public TicketType TicketType { get; set; }
    public TicketSource ticketSource { get; set; }
    public int CompanyId { get; set; }
    public IFormFile? File { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTicketCommand, Ticket>().ForMember(x => x.FilePath, opt => opt.Ignore()).ForMember(x => x.Status, opt => opt.Ignore());
    }
}
public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    private readonly ICurrentUserService _currentUserService;

    public CreateTicketCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IMapper mapper,
        IMailService mailService,
        ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
        _mailService = mailService;
        _currentUserService = currentUserService;

    }

    public async Task<Result<int>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Repository<Company>()
            .GetByIdAsync(request.CompanyId);

        if (company == null)
        {
            return Result<int>.BadRequest("Company not found.");
        }

        var customer = await _unitOfWork.Repository<User>()
       .GetByIdAsync(_currentUserService.UserId);

        if (customer == null)
        {
            return Result<int>.BadRequest("Customer not found.");
        }
        var ticket = _mapper.Map<Ticket>(request);
        ticket.CustomerId = customer.Id;
        ticket.Status = "Pending";

        if (request.File != null)
        {
            ticket.FilePath = await _fileService.GetFilePathAsync(Filetype.TicketAttachment,
                request.File,
                cancellationToken);
        }
        await _unitOfWork.Repository<Ticket>().PostAsync(ticket);

        await _unitOfWork.Save(cancellationToken);
       var mail =new MailDto
        {
            To = customer.Email,
            Subject = "Ticket Submitted Successfully",
            Body = $@"
        <h2>Hello {customer.FirstName},</h2>
        <p>Your support ticket has been submitted successfully.</p>
        <p><b>Ticket ID:</b> {ticket.Id}</p>
        <p><b>Title:</b> {ticket.TicketTitle}</p>
        <p><b>Status:</b> {ticket.Status}</p>
        <p>Our support team will contact you soon.</p>
        <br/>
        <p>Thank you,<br/>Support Team</p>"
        };

        return Result<int>.Success(ticket.Id, "Ticket Created Successfully.");
    }
}
