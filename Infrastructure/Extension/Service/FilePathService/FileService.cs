using Application.Interfaces.Services.FilePathService;
using Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Extension.Service.FilePathService;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> GetFilePathAsync(
        Filetype filetype,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            throw new Exception("No file selected.");

        var extension = Path.GetExtension(file.FileName).ToLower();

        string[] validExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".pdf",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx"
        };

        if (!validExtensions.Contains(extension))
            throw new Exception("Invalid file type.");

        if (file.Length > 5 * 1024 * 1024)
            throw new Exception("File size exceeds 5 MB.");

        if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            throw new Exception("WebRootPath is not configured.");

        string folderName = filetype switch
        {
            Filetype.ProfileImage => "Profiles",
            Filetype.CompanyLogo => "CompanyLogos",
            Filetype.TicketAttachment => "TicketAttachments",
            Filetype.ResolutionDocument => "ResolutionDocuments",
            Filetype.Invoice => "Invoices",
            _ => "Others"
        };

        string fileName = $"{Guid.NewGuid()}{extension}";

        string folderPath = Path.Combine(
            _environment.WebRootPath,
            "Uploads",
            folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return Path.Combine("Uploads", folderName, fileName).Replace("\\", "/");
    }
}