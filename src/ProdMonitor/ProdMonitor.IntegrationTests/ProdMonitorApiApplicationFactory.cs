using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.IntegrationTests.Helpers;
using Testcontainers.PostgreSql;

namespace ProdMonitor.IntegrationTests;

public class ProdMonitorApiApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithCleanUp(true)
        .WithDatabase("testdb")
        .WithUsername("testuser")
        .WithPassword("testpassword")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:DefaultConnection"] = _dbContainer.GetConnectionString()
            }!)
            .Build();
        builder.UseConfiguration(configuration);
        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var prodMonitorContext = new ProdMonitorContext(new DbContextOptionsBuilder<ProdMonitorContext>()
            .UseNpgsql(_dbContainer.GetConnectionString()).Options);
        Console.WriteLine("Starting database migrations...");
        await prodMonitorContext.Database.MigrateAsync();
        Console.WriteLine("Database migrations completed.");
        await SeedTestData(prodMonitorContext);
    }
    
    private async Task SeedTestData(ProdMonitorContext context)
    {
        //Create test users
        context.Users.Add(UserMother.Admin());
        context.Users.Add(UserMother.Operator());
        context.Users.Add(UserMother.Specialist());

        //Create test assembly lines
        context.AssemblyLines.Add(new AssemblyLineBuilder().Build());
        context.AssemblyLines.Add(new AssemblyLineBuilder()
            .WithId(Guid.Parse("a04716ca-81bb-4c46-9b6f-b4d601b1d8e0"))
            .Build());
        context.AssemblyLines.Add(new AssemblyLineBuilder()
            .WithId(Guid.NewGuid())
            .Build());
        
        //Create test details
        context.Details.Add(new DetailBuilder().Build());
        context.Details.Add(new DetailBuilder()
            .WithId(Guid.NewGuid())
            .WithName("Detail2")
            .WithCountry("USA")
            .Build());
        
        //Create test detail orders
        context.DetailOrders.Add(DetailOrderMother.DetailOrderInDelivery());
        context.DetailOrders.Add(DetailOrderMother.DetailOrderInWork());
        
        //Create test service requests
        context.ServiceRequests.Add(ServiceRequestMother.OpenServiceRequest1());
        context.ServiceRequests.Add(ServiceRequestMother.OpenServiceRequest2());
        context.ServiceRequests.Add(ServiceRequestMother.OpenServiceRequest3());
        
        //Create test service reports
        context.ServiceReports.Add(ServiceReportMother.ClosedServiceReport());
        context.ServiceReports.Add(ServiceReportMother.OpenServiceReport());
        
        //Create test tractors
        context.Tractors.Add(new TractorBuilder().Build());
        context.Tractors.Add(new TractorBuilder()
            .WithId(Guid.NewGuid())
            .WithName("Tractor2")
            .WithReleaseYear(2022)
            .WithEngineType("Gasoline")
            .Build());
        
        await context.SaveChangesAsync();
    }
    
    public void ReinitializeDbForTests(ProdMonitorContext context)
    {
        context.AssemblyLines.RemoveRange(context.AssemblyLines);
        context.Users.RemoveRange(context.Users);
        context.Details.RemoveRange(context.Details);
        context.DetailOrders.RemoveRange(context.DetailOrders);
        context.ServiceRequests.RemoveRange(context.ServiceRequests);
        context.ServiceReports.RemoveRange(context.ServiceReports);
        context.Tractors.RemoveRange(context.Tractors);
        SeedTestData(context).Wait();
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
}