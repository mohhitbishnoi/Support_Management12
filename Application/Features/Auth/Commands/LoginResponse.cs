using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string Role { get; set; }

        public string UserName { get; set; }
    }
}
