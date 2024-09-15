using ProdMonitor.DataAccess.Models.Converters.Enums;
using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class DetailOrderConverter
    {
        public static DetailOrder? ToDomain(DetailOrderDb? detailOrderDb)
        {
            if (detailOrderDb == null)
            {
                return null;
            }

            var detailOrderStatus = DetailOrderStatusTypeConverter.ToDomain(detailOrderDb.Status);

            return new DetailOrder(id: detailOrderDb.Id,
                userId: detailOrderDb.UserId,
                status: detailOrderStatus,
                totalPrice: detailOrderDb.TotalPrice,
                orderDate: detailOrderDb.OrderDate,
                orderDetails: detailOrderDb.OrderDetails?.Select(OrderDetailConverter.ToDomain).ToList() ?? new List<OrderDetail>());
        }

        public static DetailOrderDb? ToDb(DetailOrder? detailOrderDomain)
        {
            if (detailOrderDomain == null)
            {
                return null;
            }

            var detailOrderStatus = DetailOrderStatusTypeConverter.ToDb(detailOrderDomain.Status);

            return new DetailOrderDb(id: detailOrderDomain.Id,
                userId: detailOrderDomain.UserId,
                status: detailOrderStatus,
                totalPrice: detailOrderDomain.TotalPrice,
                orderDate: detailOrderDomain.OrderDate);
        }
    }
}
