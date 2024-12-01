using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Auth;

public class LoginDto(string login,
    string password)
{
    [JsonPropertyName("login")]
    [JsonRequired]
    public string Login { get; set; } = login;
    
    [JsonPropertyName("password")]
    [JsonRequired]
    public string Password { get; set; } = password;
}