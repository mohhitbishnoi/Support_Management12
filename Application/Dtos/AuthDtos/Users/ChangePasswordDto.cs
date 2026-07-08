using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.AuthDtos.Users
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
