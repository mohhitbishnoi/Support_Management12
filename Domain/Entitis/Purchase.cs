using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entitis;

public class Purchase:BaseAuditableEntity
{
    public int Quantity { get; set; }

    public DateOnly PurchaseDate { get; set; }
}
