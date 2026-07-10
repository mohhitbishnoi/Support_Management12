using System;
using System.Collections.Generic;
using System.Text;
using Domain.Commons;

namespace Domain.Entitis.Enums;

public class Company:BaseAuditableEntity
{
    public string Name {  get; set; }
    public string logo {  get; set; }
}
