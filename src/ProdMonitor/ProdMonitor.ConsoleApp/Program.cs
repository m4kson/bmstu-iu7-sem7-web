using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.DataAccess.Repositories;
using Serilog;
using Serilog.Extensions.Hosting;
using ProdMonitor.Application.Services.Configurations;

namespace ProdMonitor.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .CreateLogger();

            try
            {
                Log.Information("Starting up the application...");
                
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .UseSerilog()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<ProdMonitorContext>(options =>
                        {
                            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection"));
                        });

                        services.AddTransient<IAssemblyLineService, AssemblyLineService>();
                        services.AddTransient<IAuthenticationService, AuthenticationService>();
                        services.AddTransient<IDetailOrderService, DetailOrderService>();
                        services.AddTransient<IDetailService, DetailService>();
                        services.AddTransient<IServiceReportService, ServiceReportService>();
                        services.AddTransient<IServiceRequestService, ServiceRequestService>();
                        services.AddTransient<ITractorService, TractorService>();
                        services.AddTransient<IUserService, UserService>();

                        services.AddTransient<IAssemblyLineRepository, AssemblyLineRepository>();
                        services.AddTransient<IDetailOrderRepository, DetailOrderRepository>();
                        services.AddTransient<IDetailRepository, DetailRepository>();
                        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
                        services.AddTransient<IServiceReportRepository, ServiceReportRepository>();
                        services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
                        services.AddTransient<ITractorRepository, TractorRepository>();
                        services.AddTransient<IUserRepository, UserRepository>();

                        services.AddSingleton(Log.Logger);

                        services.Configure<AuthenticationServiceConfiguration>(
                            context.Configuration.GetSection(AuthenticationServiceConfiguration.ConfigurationSectionName)
                        );

                        services.AddTransient<Startup>();
                    })
                    .Build();

                var app = host.Services.GetRequiredService<Startup>();
                await app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
