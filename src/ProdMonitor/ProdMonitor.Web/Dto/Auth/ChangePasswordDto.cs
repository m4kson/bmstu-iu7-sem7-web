using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Auth;

public class ChangePasswordDto(string oldPassword,
    string newPassword)
{
    [JsonPropertyName("oldPassword")]
    [JsonRequired]
    public string OldPassword { get; set; } = oldPassword;
    
    [JsonPropertyName("newPassword")]
    [JsonRequired]
    public string NewPassword { get; set; } = newPassword;
}