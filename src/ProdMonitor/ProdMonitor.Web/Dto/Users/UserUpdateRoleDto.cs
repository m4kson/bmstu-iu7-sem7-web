using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Users;

public class UserUpdateRoleDto(RoleTypeDto role)
{
    [JsonPropertyName("role")]
    [JsonRequired]
    public RoleTypeDto Role { get; set; } = role;
}