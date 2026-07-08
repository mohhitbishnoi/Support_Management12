using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entitis;

public class Product: BaseAuditableEntity
{
    public string ProductName { get; set; }
    public string ProductDiscription { get; set; }
    public long Price { get; set; }
    public ICollection<Purchase> Purchase { get; set; }

    //[ForeignKey("User")]
    //public int UserId { get; set; }
    //public User? User { get; set; }
}
