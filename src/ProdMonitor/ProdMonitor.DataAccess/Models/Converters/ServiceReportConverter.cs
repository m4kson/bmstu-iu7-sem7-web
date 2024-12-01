using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class ServiceReportConverter
    {
        public static ServiceReport? ToDomain(ServiceReportDb? serviceReportDb)
        {
            if (serviceReportDb == null)
            {
                return null;
            }

            return new ServiceReport(id: serviceReportDb.Id,
                lineId: serviceReportDb.LineId,
                userId: serviceReportDb.UserId,
                requestId: serviceReportDb.RequestId,
                openDate: serviceReportDb.OpenDate,
                closeDate: serviceReportDb.CloseDate,
                price: serviceReportDb.Price,
                description: serviceReportDb.Description);
        }

        public static ServiceReportDb? ToDb(ServiceReport? serviceReportDomain)
        {
            if (serviceReportDomain == null)
            {
                return null;
            }

            return new ServiceReportDb(id: serviceReportDomain.Id,
                lineId: serviceReportDomain.LineId,
                userId: serviceReportDomain.UserId,
                requestId: serviceReportDomain.RequestId,
                openDate: serviceReportDomain.OpenDate,
                closeDate: serviceReportDomain.CloseDate,
                price: serviceReportDomain.Price,
                description: serviceReportDomain.Description);
        }
    }
}
