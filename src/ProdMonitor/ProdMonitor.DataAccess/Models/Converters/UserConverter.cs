using ProdMonitor.DataAccess.Models.Converters.Enums;
using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class UserConverter
    {
        public static User? ToDomain(UserDb? userDb)
        {
            if (userDb == null)
            {
                return null;
            }

            var sex = SexTypeConverter.ToDomain(userDb.Sex);
            var role = RoleTypeConverter.ToDomain(userDb.Role);

            return new User(id: userDb.Id,
                name: userDb.Name,
                surname: userDb.Surname,
                patronymic: userDb.Patronymic,
                department: userDb.Department,
                email: userDb.Email,
                passwordHash: userDb.PasswordHash,
                passwordSalt: userDb.PasswordSalt,
                birthDay: userDb.BirthDay,
                sex: sex,
                role: role,
                twoFactorCode: userDb.TwoFactorCode,
                twoFactorExpiration: userDb.TwoFactorExpiration);
        }

        public static UserDb? ToDb(User? userDomain)
        {
            if (userDomain == null)
            {
                return null;
            }

            var sex = SexTypeConverter.ToDb(userDomain.Sex);
            var role = RoleTypeConverter.ToDb(userDomain.Role);

            return new UserDb(id: userDomain.Id,
                name: userDomain.Name,
                surname: userDomain.Surname,
                patronymic: userDomain.Patronymic,
                department: userDomain.Department,
                email: userDomain.Email,
                passwordHash: userDomain.PasswordHash,
                passwordSalt: userDomain.PasswordSalt,
                birthDay: userDomain.BirthDay,
                sex: sex,
                role: role,
                twoFactorCode: userDomain.TwoFactorCode,
                twoFactorExpiration: userDomain.TwoFactorExpiration);

        }
    }
}
