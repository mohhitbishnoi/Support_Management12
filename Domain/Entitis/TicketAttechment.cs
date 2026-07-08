using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entitis;

public class TicketAttechment: BaseAuditableEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public DateTime UploadDate { get; set; }

    [ForeignKey("Ticket")]
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}
