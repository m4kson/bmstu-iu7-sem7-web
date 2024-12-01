using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class RequestStatusTypeConverter
    {
        public static RequestStatusType ToDomain(RequestStatusTypeDb reqeustStatusTypeDb)
        {
            return reqeustStatusTypeDb switch
            {
                RequestStatusTypeDb.Opened => RequestStatusType.Opened,
                RequestStatusTypeDb.InProgress => RequestStatusType.InProgress,
                RequestStatusTypeDb.Closed => RequestStatusType.Closed,
                _ => throw new ArgumentOutOfRangeException(nameof(reqeustStatusTypeDb), reqeustStatusTypeDb, "Incorrect enum value"),
            };
        }

        public static RequestStatusTypeDb ToDb(RequestStatusType requestStatusTypeDomain)
        {
            return requestStatusTypeDomain switch
            {
                RequestStatusType.Opened => RequestStatusTypeDb.Opened,
                RequestStatusType.InProgress => RequestStatusTypeDb.InProgress,
                RequestStatusType.Closed => RequestStatusTypeDb.Closed,
                _ => throw new ArgumentOutOfRangeException(nameof(requestStatusTypeDomain), requestStatusTypeDomain, "Incorrect enum value"),
            };
        }
    }
}
