using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class UserCreate(string name,
        string surname,
        string patronymic,
        string department,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        DateOnly birthDay,
        SexType sex,
        RoleType role)
    {
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string Patronymic { get; set; } = patronymic;
        public string Department { get; set; } = department;
        public string Email { get; set; } = email;
        public byte[] PasswordHash { get; set; } = passwordHash;
        public byte[] PasswordSalt { get; set; } = passwordSalt;
        public DateOnly BirthDay { get; set; } = birthDay;
        public SexType Sex { get; set; } = sex;
        public RoleType Role { get; set; } = role;
    }
}
