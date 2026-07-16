using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services.Users
{
    public interface ICurrentUserService
    {
        int UserId { get; }
    }
}
