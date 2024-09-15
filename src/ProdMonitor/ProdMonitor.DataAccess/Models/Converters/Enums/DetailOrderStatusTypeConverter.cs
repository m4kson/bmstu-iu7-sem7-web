using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters.Enums
{
    public static class DetailOrderStatusTypeConverter
    {
        public static DetailOrderStatusType ToDomain(DetailOrderStatusDb detailOrderStatusDb)
        {
            return detailOrderStatusDb switch
            {
                DetailOrderStatusDb.Processing => DetailOrderStatusType.Processing,
                DetailOrderStatusDb.InWork => DetailOrderStatusType.InWork,
                DetailOrderStatusDb.InDelivery => DetailOrderStatusType.InDelivery,
                DetailOrderStatusDb.Done => DetailOrderStatusType.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(detailOrderStatusDb), detailOrderStatusDb, "Incorrect enum value"),
            };
        }

        public static DetailOrderStatusDb ToDb(DetailOrderStatusType detailOrderStatusTypeDomain)
        {
            return detailOrderStatusTypeDomain switch
            {
                DetailOrderStatusType.Processing => DetailOrderStatusDb.Processing,
                DetailOrderStatusType.InWork => DetailOrderStatusDb.InWork,
                DetailOrderStatusType.InDelivery => DetailOrderStatusDb.InDelivery,
                DetailOrderStatusType.Done => DetailOrderStatusDb.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(detailOrderStatusTypeDomain), detailOrderStatusTypeDomain, "Incorrect enum value"),
            };
        }
    }
}
