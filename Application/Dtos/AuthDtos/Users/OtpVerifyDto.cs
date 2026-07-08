using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.AuthDtos.Users;

public class OtpVerifyDto
{
    public string Email { get; set; }
    public string Otp { get; set; }
}
