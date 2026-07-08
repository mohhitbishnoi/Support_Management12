using Application.Dtos.MailDtos;
using Application.Interfaces.Services.MailServices;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using IMailService = Application.Interfaces.Services.MailServices.IMailService;

namespace Infrastructure.Extension.Service.MailServices;

public class MailService : IMailService

{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> SendMail(MailDto mailDto)
    {
        var from = _configuration["MailSettings:From"];
        var password = _configuration["MailSettings:Password"];
        var port = Convert.ToInt32(_configuration["MailSettings:Port"]);
        var host = _configuration["MailSettings:Host"];

        var fromMail = new MimeKit.MailboxAddress("Mohhit", from);
        var message = new MimeKit.MimeMessage();
        message.From.Add(fromMail);

        message.To.Add(MailboxAddress.Parse(mailDto.To));
        message.Subject = mailDto.Subject;
        message.Body = new TextPart("html")
        {
            Text = mailDto.Body
        };
        var client = new SmtpClient();
        await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(from, password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        return "Mail Sent Successfully";
    }
}
