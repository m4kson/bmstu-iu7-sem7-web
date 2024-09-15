using ProdMonitor.Domain.Models.Enums;
using System.Data;

namespace ProdMonitor.Domain.Models
{
    public class RegisterModel(string email,
        string password,
        string name,
        string surname,
        string patronymic,
        SexType sex,
        string department,
        DateOnly birthDay)
    {
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string Patronymic { get; set; } = patronymic;
        public string Department { get; set; } = department;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public DateOnly BirthDay { get; set; } = birthDay;
        public SexType Sex { get; set; } = sex;
    }
}
