using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Repositories;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Application.Services;
using System;

public class ServiceTestSetup : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ServiceProvider _serviceProvider;

    public ProdMonitorContext DbContext { get; }
    public TractorService TractorService { get; }
    public ServiceRequestService ServiceRequestService { get; }
    public ServiceReportService ServiceReportService { get; }
    public AssemblyLineService AssemblyLineService { get; }
    public UserService UserService { get; }
    public AuthenticationService AuthenticationService { get; }
    public DetailOrderService DetailOrderService { get; }
    public DetailService DetailService { get; }

    public ServiceTestSetup()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<ProdMonitorContext>(options =>
            options.UseSqlite(_connection));

        services.AddScoped<ITractorRepository, TractorRepository>();
        services.AddScoped<IAssemblyLineRepository, AssemblyLineRepository>();
        services.AddScoped<IDetailRepository, DetailRepository>();
        services.AddScoped<IDetailOrderRepository, DetailOrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IServiceReportRepository, ServiceReportRepository>();
        services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<TractorService>();
        services.AddScoped<AssemblyLineService>();
        services.AddScoped<AuthenticationService>();
        services.AddScoped<DetailOrderService>();
        services.AddScoped<DetailService>();
        services.AddScoped<ServiceReportService>();
        services.AddScoped<ServiceRequestService>();
        services.AddScoped<UserService>();


        _serviceProvider = services.BuildServiceProvider();

        DbContext = _serviceProvider.GetRequiredService<ProdMonitorContext>();
        TractorService = _serviceProvider.GetRequiredService<TractorService>();
        AssemblyLineService = _serviceProvider.GetRequiredService<AssemblyLineService>();
        AuthenticationService = _serviceProvider.GetRequiredService<AuthenticationService>();
        DetailOrderService = _serviceProvider.GetRequiredService<DetailOrderService>();
        DetailService = _serviceProvider.GetRequiredService<DetailService>();
        ServiceReportService = _serviceProvider.GetRequiredService<ServiceReportService>();
        ServiceRequestService = _serviceProvider.GetRequiredService<ServiceRequestService>();
        UserService = _serviceProvider.GetRequiredService<UserService>();

        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext?.Dispose();
        _connection?.Dispose();
        _serviceProvider?.Dispose();
    }
}
