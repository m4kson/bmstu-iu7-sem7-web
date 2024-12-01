using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class AssemblyLineFilter(LineStatusType? status = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        public LineStatusType? Status { get; set; } = status;
        public int Skip { get; set; } = skip;
        public int Limit { get; set; } = limit;
    }
}
