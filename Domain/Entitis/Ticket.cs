using Domain.Commons;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entitis;
public class Ticket: BaseAuditableEntity
{
    public string TicketTitle { get; set; }
    public string TicketDescription{get; set;}
    public string Status { get; set; }
    public string TicketPriority { get; set; }
    public string? FilePath { get; set; }

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    public User? Customer { get; set; } = null;
    public Company? Company { get; set; }
    public TicketType ticketType { get; set; }
    public TicketSource ticketSource { get; set; }
    public ICollection<TicketReply> Replies { get; set; }
    public ICollection<TicketAttechment> Attechments { get; set; }
    public ICollection<TicketHistroy> TicketHistories { get; set; }

    

}

