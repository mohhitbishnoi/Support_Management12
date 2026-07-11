using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos.AuthDtos.Users;

public class ForgetPasswordDto
{
    [Required]
    public string Email { get; set; }
}
