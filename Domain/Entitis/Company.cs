using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitis;

public class Company : BaseAuditableEntity
{
    public string CompanyName { get; set; }
    public string CompanyLogo { get; set; }
}
