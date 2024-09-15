using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.GUI.Interfaces;
using ProdMonitor.GUI.Presenters;
using ProdMonitor.GUI.View;
using System.ComponentModel;
using SimpleInjector;
using System.Windows.Forms;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProdMonitor.Application.Services.Configurations;
using System;
using SimpleInjector.Lifestyles;
using Microsoft.Extensions.Logging;
using SimpleInjector.Integration.ServiceCollection;

namespace ProdMonitor.GUI
{
    internal static class Program
    {
        private static IServiceProvider _serviceProvider;

        [STAThread]
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var services = new ServiceCollection();

            services.AddSingleton<Serilog.ILogger>(Log.Logger);

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ProdMonitorContext>(options =>
                options.UseNpgsql(connectionString));

            var businessLogicSettings = configuration.GetSection("BusinessLogic").Get<AuthenticationServiceConfiguration>();
            services.AddSingleton(businessLogicSettings);

            // Регистрация репозиториев и сервисов
            services.AddScoped<IAssemblyLineRepository, AssemblyLineRepository>();
            services.AddScoped<IDetailOrderRepository, DetailOrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IServiceReportRepository, ServiceReportRepository>();
            services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddScoped<ITractorRepository, TractorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDetailRepository, DetailRepository>();
            services.AddScoped<ISupplyRepository, SupplyRepository>();

            services.AddScoped<IAssemblyLineService, AssemblyLineService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDetailOrderService, DetailOrderService>();
            services.AddScoped<IDetailService, DetailService>();
            services.AddScoped<IServiceReportService, ServiceReportService>();
            services.AddScoped<IServiceRequestService, ServiceRequestService>();
            services.AddScoped<ITractorService, TractorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISupplyService, SupplyService>();

            

            // Регистрация форм и представлений
            services.AddScoped<StartForm>();
            services.AddScoped<AuthForm>();
            services.AddScoped<RegistrationForm>();
            services.AddScoped<AdminPannel>();
           
            

            services.AddScoped<UsersForm>();
            services.AddScoped<ChangeRoleForm>();
            services.AddScoped<AddTractorForm>();
            services.AddScoped<AddLineForm>();
            services.AddScoped<AddDetailForm>();
            services.AddScoped<TractorsForm>();
            services.AddScoped<LinesForm>();
            services.AddScoped<ReportsForm>();
            services.AddScoped<DetailsForm>();
            services.AddScoped<GetDetailForm>();
            services.AddScoped<GetLineForm>();
            services.AddScoped<GetTractorForm>();
            services.AddScoped<AddSupply>();

            // Регистрация Presenter'ов
            services.AddScoped<AuthPresenter>();
            services.AddScoped<StartPresenter>();
            services.AddScoped<RegisterPresenter>();
            services.AddScoped<AdminPannelPresenter>();
            services.AddScoped<UsersPresenter>();
            services.AddScoped<AddDetailPresenter>();
            services.AddScoped<AddSupplyPresenter>();

            services.AddScoped<IStartView, StartForm>();
            services.AddScoped<IAuthView, AuthForm>();
            services.AddScoped<IRegView, RegistrationForm>();

            services.AddScoped<IAdminPannelView, AdminPannel>();
            services.AddScoped<IOperatorPannelView, OperatorPannelForm>();
            services.AddScoped<ISpecialistPannelView, SpecialistPannelForm>();
            services.AddScoped<IVerificationPannelView, VerificationPannelForm>();

            services.AddScoped<IUsersView, UsersForm>();
            services.AddScoped<IChangeRoleView, ChangeRoleForm>();
            services.AddScoped<IAddTractorView, AddTractorForm>();
            services.AddScoped<IAddLineView, AddLineForm>();
            services.AddScoped<IAddDetailView, AddDetailForm>();
            services.AddScoped<ITractorsView, TractorsForm>();
            services.AddScoped<ILinesView, LinesForm>();
            services.AddScoped<IReportsView, ReportsForm>();
            services.AddScoped<IDetailsView, DetailsForm>();
            services.AddScoped<IGetDetailView, GetDetailForm>();
            services.AddScoped<IGetLineView, GetLineForm>();
            services.AddScoped<IGetTractorView, GetTractorForm>();
            services.AddScoped<IAddSupplyView, AddSupply>();




            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            _serviceProvider = services.BuildServiceProvider();

            try
            {
                Log.Information("Starting up the application...");

                var startForm = _serviceProvider.GetRequiredService<StartForm>();
                var startPresenter = _serviceProvider.GetRequiredService<StartPresenter>();

                var authForm = _serviceProvider.GetRequiredService<AuthForm>();
                var authPresenter = _serviceProvider.GetRequiredService<AuthPresenter>();
                

                var registrationForm = _serviceProvider.GetRequiredService<RegistrationForm>();
                var registerPresenter = _serviceProvider.GetRequiredService<RegisterPresenter>();

                var adminPannelForm = _serviceProvider.GetRequiredService<AdminPannel>();
                var adminPannelPresenter = _serviceProvider.GetRequiredService<AdminPannelPresenter>();

                var usersFrom = _serviceProvider.GetRequiredService<UsersForm>();
                var usersPresenter = _serviceProvider.GetRequiredService<UsersPresenter>();

                var changeRoleForm = _serviceProvider.GetRequiredService<ChangeRoleForm>();

                var addTractorForm = _serviceProvider.GetRequiredService<AddTractorForm>();

                var addLineForm = _serviceProvider.GetRequiredService<AddLineForm>();

                var addDetailForm = _serviceProvider.GetRequiredService<AddDetailForm>();
                var addDetailPresenter = _serviceProvider.GetRequiredService<AddDetailPresenter>();

                var tractrosForm = _serviceProvider.GetRequiredService<TractorsForm>();

                var linesForm = _serviceProvider.GetRequiredService<LinesForm>();

                var detailsForm = _serviceProvider.GetRequiredService<DetailsForm>();

                var reportsForm = _serviceProvider.GetRequiredService<ReportsForm>();

                var getDetailForm = _serviceProvider.GetRequiredService<GetDetailForm>();

                var getTractorForm = _serviceProvider.GetRequiredService<GetTractorForm>();

                var getLineForm = _serviceProvider.GetRequiredService<GetLineForm>();

                var addSupplyForm = _serviceProvider.GetRequiredService<AddSupply>();
                var addSupplyPresenter = _serviceProvider.GetService<AddSupplyPresenter>();
                

                authPresenter.SetView(authForm, adminPannelForm);
                registerPresenter.SetView(registrationForm, startForm); 
                startPresenter.SetView(startForm, authForm, registrationForm);
                adminPannelPresenter.SetView(adminPannelForm,
                    usersFrom,
                    changeRoleForm,
                    addTractorForm,
                    addLineForm,
                    addDetailForm,
                    tractrosForm,
                    linesForm,
                    reportsForm,
                    detailsForm,
                    getDetailForm,
                    getLineForm,
                    getTractorForm,
                    addSupplyForm);

                usersPresenter.SetView(usersFrom, adminPannelForm);
                addDetailPresenter.SetView(addDetailForm, adminPannelForm);
                addSupplyPresenter.SetView(addSupplyForm, adminPannelForm);

                System.Windows.Forms.Application.Run(startForm);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}