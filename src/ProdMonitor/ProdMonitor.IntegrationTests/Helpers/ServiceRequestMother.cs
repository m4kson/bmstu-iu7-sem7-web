using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;

namespace ProdMonitor.IntegrationTests.Helpers;

public class ServiceRequestMother
{
    public static ServiceRequestDb OpenServiceRequest1()
    {
        return new ServiceRequestDb(
            id: Guid.Parse("ea4f7424-c870-4c2a-b1db-cf65af3d5564"),
            lineId: Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345"), 
            userId: Guid.Parse("93ba8784-b320-49d8-b810-8ad1e1bd0cf8"),
            requestDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc),
            status: RequestStatusTypeDb.Closed,
            type: RequestTypeDb.Inspection,
            description: "Service request description"
        );
    } 
    
    public static ServiceRequestDb OpenServiceRequest2()
    {
        return new ServiceRequestDb(
            id: Guid.Parse("5a700c95-55e6-4b08-9d5f-6b59a9c3e11e"),
            lineId: Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345"), 
            userId: Guid.Parse("93ba8784-b320-49d8-b810-8ad1e1bd0cf8"),
            requestDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc),
            status: RequestStatusTypeDb.Opened,
            type: RequestTypeDb.Inspection,
            description: "Service request description"
        );
    }
    
    public static ServiceRequestDb OpenServiceRequest3()
    {
        return new ServiceRequestDb(
            id: Guid.Parse("80DE302B-DDFA-4F5E-A82C-0068C7699A3D"),
            lineId: Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345"), 
            userId: Guid.Parse("93ba8784-b320-49d8-b810-8ad1e1bd0cf8"),
            requestDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc),
            status: RequestStatusTypeDb.Opened,
            type: RequestTypeDb.Inspection,
            description: "Service request description"
        );
    }
}