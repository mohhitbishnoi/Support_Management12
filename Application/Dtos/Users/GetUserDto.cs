using Application.Commons;
using Application.Commons.Mappings;
using Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users;

public class GetUserDto:BaseDto,IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public long PhoneNumber { get; set; }
    public string Password { get; set; }

    public string? RoleName{ get; set; }
}
