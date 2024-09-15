namespace ProdMonitor.Domain.Models
{
    public class DetailFilter(string? country,
        int skip = 0,
        int limit = int.MaxValue)
    {
        public string? Country { get; set; } = country;
        public int Skip { get; set; } = skip;
        public int Limit { get; set; } = limit;
    }
}
