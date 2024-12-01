using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class RoleTypeDtoConverter
{
    public static RoleTypeDto ToDto(this RoleType roleType)
    {
        return roleType switch
        {
            RoleType.Admin => RoleTypeDto.Admin,
            RoleType.Operator => RoleTypeDto.Operator,
            RoleType.Specialist => RoleTypeDto.Specialist,
            RoleType.Verification => RoleTypeDto.Verification,
            _ => throw new ArgumentOutOfRangeException(nameof(roleType), roleType, "Incorrect enum value")
        };
    }
    
    public static RoleType ToDomain(this RoleTypeDto roleTypeDto)
    {
        return roleTypeDto switch
        {
            RoleTypeDto.Admin => RoleType.Admin,
            RoleTypeDto.Operator => RoleType.Operator,
            RoleTypeDto.Specialist => RoleType.Specialist,
            RoleTypeDto.Verification => RoleType.Verification,
            _ => throw new ArgumentOutOfRangeException(nameof(roleTypeDto), roleTypeDto, "Incorrect enum value")
        };
    }
}