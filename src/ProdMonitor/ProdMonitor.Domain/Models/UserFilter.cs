using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class UserFilter(string? department = null,
        DateOnly? birthDay = null,
        SexType? sex = null,
        RoleType? role = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        public string? Department { get; set; } = department;
        public DateOnly? BirthDay { get; set; } = birthDay;
        public SexType? Sex { get; set; } = sex;
        public RoleType? Role { get; set; } = role;
        public int Skip { get; set; } = skip;
        public int Limit { get; set; } = limit;
    }
}
