using Domain.Commons;

namespace Domain.Entitis
{
    public class User : BaseAuditableEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;
    }
}