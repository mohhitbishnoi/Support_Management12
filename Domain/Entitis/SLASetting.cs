using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitis;

public class SlASetting:BaseAuditableEntity
{
    public string Priority { get; set; }
    public int ResponseTimeHours { get; set; }
    public int ResolutionTimeHours { get; set; }
}
