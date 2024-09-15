using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class RequestTypeConverter
    {
        public static RequestType ToDomain(RequestTypeDb requestTypeDb)
        {
            return requestTypeDb switch
            {
                RequestTypeDb.Inspection => RequestType.Inspection,
                RequestTypeDb.Repair => RequestType.Repair,
                _ => throw new ArgumentOutOfRangeException(nameof(requestTypeDb), requestTypeDb, "Incorrect enum value"),
            };
        }

        public static RequestTypeDb ToDb(RequestType requestTypeDomain)
        {
            return requestTypeDomain switch
            {
                RequestType.Inspection => RequestTypeDb.Inspection,
                RequestType.Repair => RequestTypeDb.Repair,
                _ => throw new ArgumentOutOfRangeException(nameof(requestTypeDomain), requestTypeDomain, "Incorrect enum value"),
            };
        }
    }
}
