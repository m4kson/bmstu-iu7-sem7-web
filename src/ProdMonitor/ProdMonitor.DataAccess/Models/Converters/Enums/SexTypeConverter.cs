using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class SexTypeConverter
    {
        public static SexType ToDomain(SexTypeDb sexTypeDb)
        {
            return sexTypeDb switch
            {
                SexTypeDb.Male => SexType.Male,
                SexTypeDb.Female => SexType.Female,
                _ => throw new ArgumentOutOfRangeException(nameof(sexTypeDb), sexTypeDb, "Incorrect enum value"),
            };
        }

        public static SexTypeDb ToDb(SexType sexTypeDomin)
        {
            return sexTypeDomin switch
            {
               SexType.Male => SexTypeDb.Male,
               SexType.Female => SexTypeDb.Female,
                _ => throw new ArgumentOutOfRangeException(nameof(sexTypeDomin), sexTypeDomin, "Incorrect enum value"),
            };
        }
    }
}
