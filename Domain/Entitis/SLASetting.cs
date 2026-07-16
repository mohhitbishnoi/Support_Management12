using Domain.Commons;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitis;

public class SlASetting:BaseAuditableEntity
{
    public TicketPriority Priority { get; set; }
    public int ResponseTimeHours { get; set; }
    public int ResolutionTimeHours { get; set; }
}
