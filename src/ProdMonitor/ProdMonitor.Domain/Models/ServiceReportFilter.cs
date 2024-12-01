namespace ProdMonitor.Domain.Models
{
    public class ServiceReportFilter(Guid? lineId = null,
        Guid? userId = null,
        Guid? requestId = null,
        bool? sortByDate = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        public Guid? LineId { get; set; } = lineId;
        public Guid? UserId { get; set; } = userId;
        public Guid? RequestId { get; set; } = requestId;
        public bool? SortByDate { get; set; } = sortByDate;
        public int Skip { get; set; } = skip;
        public int Limit { get; set; } = limit;
    }
}
