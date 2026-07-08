using Application.Dtos.TicketDtos;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles_Access.Admin;

public class ViewAllTicketsQuery:IRequest<Result<List<GetAllTicketDto>>>
{
    internal class ViewAllTicketsQueryHandler
        : IRequestHandler<ViewAllTicketsQuery, Result<List<GetAllTicketDto>>>
    {
        private readonly IUnitOfWork _unitOfWrok;
        
        public ViewAllTicketsQueryHandler(IUnitOfWork unitOfWrok)
        {
            _unitOfWrok = unitOfWrok;
        }
        public async Task<Result<List<GetAllTicketDto>>>Handle(ViewAllTicketsQuery request,CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWrok
               .Repository<Ticket>()
               .Entities
               .Where(x => !x.IsDeleted)
               .Select(x => new Ticket
               {
                   Id = x.Id,
                   TicketTitle = x.TicketTitle,
                   TicketDescription = x.TicketDescription,
                   Status = x.Status,
                   CreateDate = x.CreateDate
               })
                .ToListAsync(cancellationToken);
            return Result<List<GetAllTicketDto>>.Success("All Tickets");
        }
    }
}
