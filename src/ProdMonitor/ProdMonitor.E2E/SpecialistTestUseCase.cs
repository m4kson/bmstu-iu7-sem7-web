using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web;
using ProdMonitor.Web.Dto.Reports;
using ProdMonitor.Web.Dto.Users;

namespace ProdMonitor.E2E;

public class SpecialistTestUseCase: IClassFixture<E2EApplicationFactory>
{
    private readonly E2EApplicationFactory _factory;
    
    public SpecialistTestUseCase(E2EApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(5)] 
    public async Task SpecialistTestUseCase_WithCorrectData(int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProdMonitorContext>();
                _factory.ReinitializeDbForTests(db);
            }
            // Arrange
            using var httpClient = _factory.CreateClient();
            var userData = new 
            {
                Login = "specialist@mail.ru",
                Password = "specialist"
            };
            
            var content = new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8, "application/json");
            
            // Act
            using var responseLogin = await httpClient.PostAsync("api/v1/Auth/login", content);
            var resultLogin = await responseLogin.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(resultLogin);
            
            using var responseRequests = await httpClient.GetAsync("api/v1/ServiceRequests");
            var resultRequests = await responseRequests.Content.ReadAsStringAsync();
            
            var serviceReportCreateDto = new
            {
                UserId = user.Id.ToString(),
                RequestId = "80DE302B-DDFA-4F5E-A82C-0068C7699A3D"
            };
            var contentReport = new StringContent(JsonConvert.SerializeObject(serviceReportCreateDto), Encoding.UTF8, "application/json");

            using var responseReportCreate = await httpClient.PostAsync("api/v1/ServiceReports", contentReport);
            var resultReportCreate = await responseReportCreate.Content.ReadAsStringAsync();
            var report = JsonConvert.DeserializeObject<ReportDto>(resultReportCreate);

            Guid id = report.LineId;
            using var responseGetLine = await httpClient.GetAsync($"api/v1/AssemblyLines/{id}");
            var resultGetLine = await responseGetLine.Content.ReadAsStringAsync();
            
            
            var closeData = new
            {
                TotalPrice = "1000",
                Description = "Test"
            };
            var contentCloseReport = new StringContent(JsonConvert.SerializeObject(closeData), Encoding.UTF8, "application/json");
            
            using var responseCloseReport = await httpClient.PatchAsync($"api/v1/ServiceReports/close/{report.Id}", contentCloseReport);
            
            // Assert
            responseLogin.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
            resultLogin.Should().NotBeNullOrEmpty(); 
            Assert.Equal(user.Name, "specialist");
            
            responseRequests.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
            resultRequests.Should().NotBeNullOrEmpty();
            
            responseReportCreate.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
            
            responseGetLine.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
            resultGetLine.Should().NotBeNullOrEmpty();
            
            responseCloseReport.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        }
    }
}