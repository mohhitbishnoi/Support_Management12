using Domain.Commons;

namespace Domain.Entitis;

public class Role:BaseAuditableEntity
{
    public string RoleName { get; set; }
    public string? RoleDescription { get; set; }
    public ICollection<User> users { get; set; }
}
