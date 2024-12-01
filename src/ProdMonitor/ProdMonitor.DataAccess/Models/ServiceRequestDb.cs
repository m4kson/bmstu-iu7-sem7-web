using System;
using System.Collections.Generic;
using ProdMonitor.DataAccess.Models.Enums;

namespace ProdMonitor.DataAccess.Models
{
    public class ServiceRequestDb
    {
        public Guid Id { get; set; }
        public Guid LineId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public RequestStatusTypeDb Status { get; set; }
        public RequestTypeDb Type { get; set; }
        public string Description { get; set; }

        public virtual UserDb? User { get; set; }
        public virtual AssemblyLineDb? AssemblyLine { get; set; }
        public Guid? ServiceReportId { get; set; }
        public virtual ServiceReportDb? ServiceReport { get; set; }

        public ServiceRequestDb(Guid id,
                                Guid lineId,
                                Guid userId,
                                DateTime requestDate,
                                RequestStatusTypeDb status,
                                RequestTypeDb type,
                                string description)
        {
            Id = id;
            LineId = lineId;
            UserId = userId;
            RequestDate = requestDate;
            Status = status;
            Type = type;
            Description = description;
        }

        public ServiceRequestDb()
        {

        }
    }
}
