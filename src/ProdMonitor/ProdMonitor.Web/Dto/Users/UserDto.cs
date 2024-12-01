using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Users;

public class UserDto(
    Guid id,
    string name,
    string surname,
    string fathername,
    string department,
    string email,
    byte[] passwordHash,
    byte[] passwordSalt,
    DateOnly birthDay,
    SexTypeDto sex,
    RoleTypeDto role)
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public Guid Id { get; set; } = id;

    [JsonPropertyName("name")]
    [JsonRequired]
    public string Name { get; set; } = name;

    [JsonPropertyName("surname")]
    [JsonRequired]
    public string Surname { get; set; } = surname;

    [JsonPropertyName("fathername")]
    [JsonRequired]
    public string Fathername { get; set; } = fathername;

    [JsonPropertyName("department")]
    [JsonRequired]
    public string Department { get; set; } = department;

    [JsonPropertyName("email")]
    [JsonRequired]
    public string Email { get; set; } = email;

    [JsonPropertyName("passwordHash")]
    [JsonRequired]
    public byte[] PasswordHash { get; set; } = passwordHash;

    [JsonPropertyName("passwordSalt")]
    [JsonRequired]
    public byte[] PasswordSalt { get; set; } = passwordSalt;

    [JsonPropertyName("birthDay")]
    [JsonRequired]
    public DateOnly BirthDay { get; set; } = birthDay;

    [JsonPropertyName("sex")]
    [JsonRequired]
    public SexTypeDto Sex { get; set; } = sex;

    [JsonPropertyName("role")]
    [JsonRequired]
    public RoleTypeDto Role { get; set; } = role;
}