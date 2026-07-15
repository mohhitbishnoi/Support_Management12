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
        var from = _configuration["EmailSettings:Email"];
        var password = _configuration["EmailSettings:Password"];
        var host = _configuration["EmailSettings:Host"];
        var portString = _configuration["EmailSettings:Port"];

        if (string.IsNullOrWhiteSpace(from))
            throw new Exception("EmailSettings:Email is missing.");

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("EmailSettings:Password is missing.");

        if (string.IsNullOrWhiteSpace(host))
            throw new Exception("EmailSettings:Host is missing.");

        if (!int.TryParse(portString, out var port))
            throw new Exception("EmailSettings:Port is invalid.");

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
