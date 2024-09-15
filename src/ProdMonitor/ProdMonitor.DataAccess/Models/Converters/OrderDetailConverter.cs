using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class OrderDetailConverter
    {
        public static OrderDetail? ToDomain(OrderDetailDb? orderDetailDb)
        {
            if (orderDetailDb == null)
            {
                return null;
            }

            return new OrderDetail(id: orderDetailDb.Id,
                detailId: orderDetailDb.DetailId,
                detailOrderId: orderDetailDb.DetailOrderId,
                detailsAmount: orderDetailDb.DetailsAmount);
        }

        public static OrderDetailDb? ToDb(OrderDetail? orderDetailDomain)
        {
            if (orderDetailDomain == null)
            {
                return null;
            }

            return new OrderDetailDb(id: orderDetailDomain.Id,
                detailId: orderDetailDomain.DetailId,
                detailOrderId: orderDetailDomain.DetailOrderId,
                detailsAmount: orderDetailDomain.DetailsAmount);
        }
    }
}
