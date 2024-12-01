using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class UserGet(Guid id,
        string name,
        string surname,
        string patronymic,
        string department,
        string email,
        DateOnly birthDay,
        SexType sex,
        RoleType role)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string Patronymic { get; set; } = patronymic;
        public string Department { get; set; } = department;
        public string Email { get; set; } = email;
        public DateOnly BirthDay { get; set; } = birthDay;
        public SexType SexType { get; set; } = sex;
        public RoleType Role { get; set; } = role;
    }
}
