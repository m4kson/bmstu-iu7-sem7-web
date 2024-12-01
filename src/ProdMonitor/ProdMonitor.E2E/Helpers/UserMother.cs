using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.E2E.Helpers;

public class UserMother
{
    public static UserDb Admin()
    {
        return new UserDb(
            id: Guid.Parse("203b583b-fe1d-4e1c-b3c6-9408b907bd38"),
            name: "admin",
            surname: "string",
            patronymic: "string",
            department: "IT",
            email: "admin@mail.ru",
            passwordHash: new byte[] {1, 2, 3},
            passwordSalt: new byte[] {4, 5, 6},
            birthDay: new DateOnly(1990, 1, 1),
            sex: SexTypeDb.Female,
            role: RoleTypeDb.Admin);
    }

    public static UserDb Operator()
    {
        return new UserDb(
            id: Guid.Parse("93ba8784-b320-49d8-b810-8ad1e1bd0cf8"),
            name: "operator",
            surname: "string",
            patronymic: "string",
            department: "IT",
            email: "operator@mail.ru",
            passwordHash: new byte[] {1, 2, 3},
            passwordSalt: new byte[] {4, 5, 6},
            birthDay: new DateOnly(1990, 1, 1),
            sex: SexTypeDb.Male,
            role: RoleTypeDb.Operator);
    }

    public static UserDb Specialist()
    {
        string password = "specialist";
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);
        return new UserDb(
            id: Guid.Parse("a991df27-a0ff-4a4e-8173-fcf3a40befb0"),
            name: "specialist",
            surname: "string",
            patronymic: "string",
            department: "IT",
            email: "specialist@mail.ru",
            passwordHash: passwordHash,
            passwordSalt: passwordSalt,
            birthDay: new DateOnly(1990, 1, 1),
            sex: SexTypeDb.Male,
            role: RoleTypeDb.Specialist);
    }
    
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}