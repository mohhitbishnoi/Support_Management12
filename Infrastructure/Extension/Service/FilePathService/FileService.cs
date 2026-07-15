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

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

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
            throw new Exception("File size cannot exceed 5 MB.");

        // WebRootPath agar null ho to ContentRootPath/wwwroot use karo
        string rootPath = _environment.WebRootPath;

        if (string.IsNullOrWhiteSpace(rootPath))
        {
            rootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
        }

        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        string folderName = filetype switch
        {
            Filetype.ProfileImage => "Profiles",
            Filetype.CompanyLogo => "CompanyLogos",
            Filetype.TicketAttachment => "TicketAttachments",
            Filetype.ResolutionDocument => "ResolutionDocuments",
            Filetype.Invoice => "Invoices",
            _ => "Others"
        };

        string uploadFolder = Path.Combine(rootPath, "Uploads", folderName);

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string fileName = $"{Guid.NewGuid()}{extension}";
        string fullPath = Path.Combine(uploadFolder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return $"Uploads/{folderName}/{fileName}";
    }
}