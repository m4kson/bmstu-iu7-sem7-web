using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class RoleTypeConverter
    {
        public static RoleType ToDomain(RoleTypeDb roleTypeDb)
        {
            return roleTypeDb switch
            {
                RoleTypeDb.Operator => RoleType.Operator,
                RoleTypeDb.Admin => RoleType.Admin,
                RoleTypeDb.Specialist => RoleType.Specialist,
                RoleTypeDb.Verification => RoleType.Verification,
                _ => throw new ArgumentOutOfRangeException(nameof(roleTypeDb), roleTypeDb, "Incorrect enum value"),
            };
        }

        public static RoleTypeDb ToDb(RoleType roleType)
        {
            return roleType switch
            {
                RoleType.Operator => RoleTypeDb.Operator,
                RoleType.Admin => RoleTypeDb.Admin,
                RoleType.Specialist => RoleTypeDb.Specialist,
                RoleType.Verification => RoleTypeDb.Verification,
                _ => throw new ArgumentOutOfRangeException(nameof(roleType), roleType, "Incorrect enum value"),
            };
        }
    }
}
