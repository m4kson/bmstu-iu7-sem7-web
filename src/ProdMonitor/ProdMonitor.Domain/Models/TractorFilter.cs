namespace ProdMonitor.Domain.Models
{
    public class TractorFilter(int? releaseYear = null,
        string? engineType = null,
        string? ecologicalStandart = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        public int? ReleaseYear { get; set; } = releaseYear;
        public string? EngineType { get; set; } = engineType;
        public string? EcologicalStandart { get; set; } = ecologicalStandart;
        public int Skip { get; set; } = skip;
        public int Limit { get; set; } = limit;
    }
}
