using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Auth;

public class ChangePasswordDto(Guid userId,
    string oldPassword,
    string newPassword)
{
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("oldPassword")]
    [JsonRequired]
    public string OldPassword { get; set; } = oldPassword;
    
    [JsonPropertyName("newPassword")]
    [JsonRequired]
    public string NewPassword { get; set; } = newPassword;
}