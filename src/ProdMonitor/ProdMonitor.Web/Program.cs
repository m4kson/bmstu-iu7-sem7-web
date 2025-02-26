using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Extensions.Hosting;
using ProdMonitor.Application.Services;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Repositories;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Interfaces.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
});

builder.Services.AddDbContext<ProdMonitorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProdMonitorAPI", Version = "v1" });
});


builder.Services.AddSingleton(Log.Logger);
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddTransient<IAssemblyLineService, AssemblyLineService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDetailOrderService, DetailOrderService>();
builder.Services.AddTransient<IDetailService, DetailService>();
builder.Services.AddTransient<IServiceReportService, ServiceReportService>();
builder.Services.AddTransient<IServiceRequestService, ServiceRequestService>();
builder.Services.AddTransient<ITractorService, TractorService>();

builder.Services.AddTransient<IAssemblyLineRepository, AssemblyLineRepository>();
builder.Services.AddTransient<IDetailOrderRepository, DetailOrderRepository>();
builder.Services.AddTransient<IDetailRepository, DetailRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddTransient<IServiceReportRepository, ServiceReportRepository>();
builder.Services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddTransient<ITractorRepository, TractorRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

//OpenTelemetry

var resource = ResourceBuilder.CreateDefault().AddService("ProdMonitorAPI");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("ProdMonitorAPI")
        .AddAttributes(new Dictionary<string, object>
        {
            { "service.type", "application" }
        }))
    .WithMetrics(metrics =>
    {
        metrics
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
    });

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("ProdMonitorAPI-Monitoring")
        .AddAttributes(new Dictionary<string, object>
        {
            { "service.type", "monitoring" }
        }))
    .WithMetrics(metrics =>
    {
        metrics
            .AddProcessInstrumentation()
            .AddPrometheusExporter();
    });

builder.Logging.AddOpenTelemetry(logging =>
    logging
        .SetResourceBuilder(resource)
        //.AddConsoleExporter()
        .AddOtlpExporter(otlpOptions =>
        {
            otlpOptions.Endpoint =
                new Uri("http://prodmonitor.dashboard:18889");
        })
    );
    

var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProdMonitorContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "api/v1";
    });
}

//app.UseStaticFiles();

//app.UseHttpsRedirection();

app.MapControllers();

app.UseDeveloperExceptionPage();

app.Run();

public partial class Program { }