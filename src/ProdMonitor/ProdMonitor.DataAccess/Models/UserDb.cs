using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.DataAccess.Models
{
    public class UserDb
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
        public SexTypeDb Sex { get; set; }
        public RoleTypeDb Role { get; set; }

        public virtual ICollection<DetailOrderDb>? DetailOrders { get; set; } = [];
        public virtual ICollection<ServiceRequestDb>? ServiceRequests { get; set; } = [];
        public virtual ICollection<ServiceReportDb>? ServiceReports { get; set; } = [];
    
        public UserDb(Guid id,
            string name,
            string surname,
            string patronymic,
            string department,
            string email,
            byte[] passwordHash,
            byte[] passwordSalt,
            DateOnly birthDay,
            SexTypeDb sex,
            RoleTypeDb role) 
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
