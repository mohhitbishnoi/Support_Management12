using Application.Commons;
using Application.Commons.Mappings;
using Domain.Entitis;
using Domain.Enum;

namespace Application.Dtos.Users;

public class GetUserDto : BaseDto, IMapFrom<User>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public long PhoneNumber { get; set; }

    public string Password { get; set; }

  
    public RoleId RoleId { get; set; }

    public string? RoleName { get; set; }
}