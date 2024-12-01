using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Users;

public class UserUpdateDto(string? name = null,
    string? surname = null,
    string? fathername = null,
    string? department = null,
    string? email = null,
    string? password = null,
    DateOnly? birthDate = null,
    SexTypeDto? sex = null)
{
    [JsonPropertyName("name")]
    public string? Name { get; set; } = name;
    
    [JsonPropertyName("surname")]
    public string? Surname { get; set; } = surname;
    
    [JsonPropertyName("fathername")]
    public string? Fathername { get; set; } = fathername;
    
    [JsonPropertyName("department")]
    public string? Department { get; set; } = department;
    
    [JsonPropertyName("email")]
    public string? Email { get; set; } = email;
    
    [JsonPropertyName("password")]
    public string? Password { get; set; } = password;
    
    [JsonPropertyName("birthDate")]
    public DateOnly? BirthDate { get; set; } = birthDate;

    [JsonPropertyName("sex")] 
    public SexTypeDto? Sex { get; set; } = sex;
}