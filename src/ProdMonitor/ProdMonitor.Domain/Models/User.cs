using System.Security.Principal;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; } 
        public string Department { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateOnly BirthDay { get; set; } 
        public SexType Sex { get; set; } 
        public RoleType Role { get; set; } 

        public User(Guid id,
            string name,
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
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Department = department;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            BirthDay = birthDay;
            Sex = sex;
            Role = role;
        }
    }
}
