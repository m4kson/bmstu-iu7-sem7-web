using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class DetailConverter
    {
        public static Detail? ToDomain(DetailDb? detailDb)
        {
            if (detailDb == null)
            {
                return null;
            }

            return new Detail(id: detailDb.Id,
                name: detailDb.Name,
                country: detailDb.Country,
                amount: detailDb.Amount,
                price: detailDb.Price,
                length: detailDb.Length,
                height: detailDb.Height,
                width: detailDb.Width,
                assemblyLines: detailDb.AssemblyLines?.Select(AssemblyLineConverter.ToDomain).ToList() ?? new List<AssemblyLine?>(),
                orderDetails: detailDb.OrderDetails?.Select(OrderDetailConverter.ToDomain).ToList() ?? new List<OrderDetail>());

        }

        public static DetailDb? ToDb(Detail detailDomain)
        {
            if (detailDomain == null)
            {
                return null;
            }

            return new DetailDb(id: detailDomain.Id,
                name: detailDomain.Name,
                country: detailDomain.Country,
                amount: detailDomain.Amount,
                price: detailDomain.Price,
                length: detailDomain.Length,
                height: detailDomain.Height,
                width: detailDomain.Width);
        }
    }
}
