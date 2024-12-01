using ProdMonitor.DataAccess.Models;

namespace ProdMonitor.IntegrationTests.Helpers;

public class ServiceReportMother
{
    public static ServiceReportDb ClosedServiceReport()
    {
        return new ServiceReportDb(
            id: Guid.Parse("938d9d85-4665-4d30-b549-fef4593c8a78"),
            lineId: Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345"),
            userId: Guid.Parse("a991df27-a0ff-4a4e-8173-fcf3a40befb0"),
            requestId: Guid.Parse("5a700c95-55e6-4b08-9d5f-6b59a9c3e11e"),
            openDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc),
            closeDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 5, 0), DateTimeKind.Utc),
            price: 100,
            description: "Service report description"
        );
    }
    
    public static ServiceReportDb OpenServiceReport()
    {
        return new ServiceReportDb(
            id: Guid.Parse("5a700c95-55e6-4b08-9d5f-6b59a9c3e11e"),
            lineId: Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345"),
            userId: Guid.Parse("a991df27-a0ff-4a4e-8173-fcf3a40befb0"),
            requestId: Guid.Parse("ea4f7424-c870-4c2a-b1db-cf65af3d5564"),
            openDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc),
            closeDate: null,
            price: null,
            description: null
        );
    }
}