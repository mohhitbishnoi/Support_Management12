using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.MailDtos;

public class MailDto
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
