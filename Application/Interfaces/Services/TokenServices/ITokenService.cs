using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services.TokenServices;

public interface ITokenService
{
    Task<string> GenerateToken(int UserId, string Role);
}
