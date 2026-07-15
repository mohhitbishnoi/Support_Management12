using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services.FilePathService;

public interface IFileService
{
    Task<string> GetFilePathAsync(Filetype filetype, IFormFile file,CancellationToken cancellationToken);

}
