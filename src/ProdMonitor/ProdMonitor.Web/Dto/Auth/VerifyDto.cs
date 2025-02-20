using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Auth;

public class VerifyDto(Guid userId, string twoFactorCode)
{
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("twoFactorCode")]
    [JsonRequired] 
    public string TwoFactorCode { get; set; } = twoFactorCode;
}