using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class UserUpdateRole(RoleType role)
    {
        public RoleType Role { get; set; } = role;
    }
}
