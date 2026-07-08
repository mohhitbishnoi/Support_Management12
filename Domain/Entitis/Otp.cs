using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entitis;

public class Otp: BaseAuditableEntity
{
    public int OtpCode {  get; set; }

    public bool IsUsed { get; set; }

    public DateTime Time { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

}
