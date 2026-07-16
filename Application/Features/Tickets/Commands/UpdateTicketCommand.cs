using Application.Commons.Mappings;
using Application.Interfaces.Repository;
using Application.Interfaces.Services.FilePathService;
using AutoMapper;
using Domain.Entitis;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;


namespace Application.Features.Tickets.TicketCommands;

public class UpdateTicketCommand : IRequest<Result<int>>, ICreateMapFrom<Ticket>
{
    public int TicketId { get; set; }
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public string Status { get; set; }
    public int CompanyId { get; set; }

    public  TicketPriority ticketPriority { get; set; }
    public TicketType TicketType { get; set; }
    public TicketSource ticketSource { get; set; }

    public IFormFile? File { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateTicketCommand, Ticket>()
            .ForMember(x => x.FilePath, opt => opt.Ignore());
    }
}

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public UpdateTicketCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Repository<Ticket>()
            .GetByIdAsync(request.TicketId);

        if (ticket == null)
        {
            return Result<int>.BadRequest("Ticket not found.");
        }

        var company = await _unitOfWork.Repository<Company>()
            .GetByIdAsync(request.CompanyId);

        if (company == null)
        {
            return Result<int>.BadRequest("Company not found.");
        }

        ticket.TicketTitle = request.TicketTitle;
        ticket.TicketDescription = request.TicketDescription;
        ticket.Status = request.Status;
        ticket.CompanyId = request.CompanyId;

        if (request.File != null)
        {
            ticket.FilePath = await _fileService.GetFilePathAsync(
                Filetype.TicketAttachment,
                request.File,
                cancellationToken);
        }

        await _unitOfWork.Repository<Ticket>()
            .PutAsync(ticket.Id, ticket);

        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(ticket.Id, "Ticket Updated Successfully.");
    }
}