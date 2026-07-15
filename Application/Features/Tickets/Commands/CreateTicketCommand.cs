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

public class CreateTicketCommand : IRequest<Result<int>>, ICreateMapFrom<Ticket>
{
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public string TicketPriority { get; set; }
    public int CompanyId { get; set; }
    public IFormFile? File { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTicketCommand, Ticket>()
            .ForMember(x => x.FilePath, opt => opt.Ignore())
            .ForMember(x => x.Status, opt => opt.Ignore());
    }
}

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Repository<Company>()
            .GetByIdAsync(request.CompanyId);

        if (company == null)
        {
            return Result<int>.BadRequest("Company not found.");
        }

        var ticket = _mapper.Map<Ticket>(request);

        ticket.Status = "Pending";

        if (request.File != null)
        {
            ticket.FilePath = await _fileService.GetFilePathAsync(
                Filetype.TicketAttachment,
                request.File,
                cancellationToken);
        }

        await _unitOfWork.Repository<Ticket>().PostAsync(ticket);

        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(ticket.Id, "Ticket Created Successfully.");
    }
}