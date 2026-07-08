using Application.Dtos.MailDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services.MailServices
{
    public interface IMailService
    {
        Task<string> SendMail(MailDto mailDto);
    }
}
