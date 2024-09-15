using ProdMonitor.DataAccess.Models.Converters.Enums;
using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class ServiceRequestConverter
    {
        public static ServiceRequest? ToDomain(ServiceRequestDb? serviceRequestDb)
        {
            if (serviceRequestDb == null)
            {
                return null;
            }

            var requestStatusType = RequestStatusTypeConverter.ToDomain(serviceRequestDb.Status);
            var requestType = RequestTypeConverter.ToDomain(serviceRequestDb.Type);

            return new ServiceRequest(id: serviceRequestDb.Id,
                lineId: serviceRequestDb.LineId,
                userId: serviceRequestDb.UserId,
                requestDate: serviceRequestDb.RequestDate,
                status: requestStatusType,
                type: requestType,
                description: serviceRequestDb.Description);
        }

        public static ServiceRequestDb? ToDb(ServiceRequest? serviceRequestDomain)
        {
            if (serviceRequestDomain == null)
            {
                return null;
            }

            var requestStatusType = RequestStatusTypeConverter.ToDb(serviceRequestDomain.Status);
            var requestType = RequestTypeConverter.ToDb(serviceRequestDomain.Type);


            return new ServiceRequestDb(id: serviceRequestDomain.Id,
                lineId: serviceRequestDomain.LineId,
                userId: serviceRequestDomain.UserId,
                requestDate: serviceRequestDomain.RequestDate,
                status: requestStatusType,
                type: requestType,
                description: serviceRequestDomain.Description);
        }
    }
}
