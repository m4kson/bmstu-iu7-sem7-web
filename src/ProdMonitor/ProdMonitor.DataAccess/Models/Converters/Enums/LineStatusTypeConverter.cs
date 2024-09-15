using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class LineStatusTypeConverter
    {
        public static LineStatusType ToDomain(LineStatusTypeDb lineStatusTypeDb)
        {
            return lineStatusTypeDb switch
            {
                LineStatusTypeDb.Working => LineStatusType.Working,
                LineStatusTypeDb.OnService => LineStatusType.OnService,
                _ => throw new ArgumentOutOfRangeException(nameof(lineStatusTypeDb), lineStatusTypeDb, "Incorrect enum value"),
            };
        }

        public static LineStatusTypeDb ToDb(LineStatusType lineStatusTypeDomain)
        {
            return lineStatusTypeDomain switch
            {
                LineStatusType.Working => LineStatusTypeDb.Working,
                LineStatusType.OnService => LineStatusTypeDb.OnService,
                _ => throw new ArgumentOutOfRangeException(nameof(lineStatusTypeDomain), lineStatusTypeDomain, "Incorrect enum value"),
            };
        }
    }
}
