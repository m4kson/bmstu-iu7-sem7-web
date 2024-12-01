using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Auth;

public class RegisterDto(string name,
    string surname,
    string fathername,
    string department,
    string email,
    string password,
    DateOnly birthDate,
    SexTypeDto sex)
{
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
    
    [JsonPropertyName("password")]
    [JsonRequired]
    public string Password { get; set; } = password;
    
    [JsonPropertyName("birthDate")]
    [JsonRequired]
    public DateOnly BirthDate { get; set; } = birthDate;

    [JsonPropertyName("sex")]
    [JsonRequired]
    public SexTypeDto Sex { get; set; } = sex;
}