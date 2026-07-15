using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitis;

public class TicketType : BaseAuditableEntity
{
    public string TicketTypeName { get; set; }
    public string TicketTypeDescription { get; set; }
    public ICollection<Ticket> tickets { get; set; }
}
