using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using Serilog;
using System.ComponentModel;


namespace ProdMonitor.Application.Services
{
    public class ServiceReportService : IServiceReportService
    {
        private readonly IServiceReportRepository _serviceReportRepository;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IAssemblyLineRepository _assemblyLineRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public ServiceReportService(IServiceReportRepository serviceReportRepository, 
            IServiceRequestRepository serviceRequestRepository,
            IAssemblyLineRepository assemblyLineRepository,
            IUserRepository userRepository,
            ILogger logger)
        {
            _serviceReportRepository = serviceReportRepository;
            _serviceRequestRepository = serviceRequestRepository;
            _assemblyLineRepository = assemblyLineRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ServiceReport> CreateServiceReportAsync(ServiceReportCreate report)
        {
            _logger.Information("Attempting to create a service report for request ID {RequestId}", report.RequestId);
            try
            {
                var request = await _serviceRequestRepository.GetServiceRequestByIdAsync(report.RequestId);
                if (request == null)
                {
                    _logger.Warning("Request with ID {RequestId} not found", report.RequestId);
                    throw new ArgumentException($"Request with id {report.RequestId} not found");
                }

                if (request.Status != RequestStatusType.Opened)
                {
                    _logger.Warning("Request with ID {RequestId} is not opened", report.RequestId);
                    throw new ArgumentException($"Request with id {report.RequestId} not opened");
                }


                var line = await _assemblyLineRepository.GetAssemblyLineByIdAsync(request.LineId);
                if (line == null)
                {
                    _logger.Warning("Assembly line with ID {LineId} not found", request.LineId);
                    throw new ArgumentException($"Assembly line with id {request.LineId} not found");
                }

                var user = await _userRepository.GetUserByIdAsync(report.UserId);
                if (user == null)
                {
                    _logger.Warning("User with ID {UserId} not found", report.UserId);
                    throw new ArgumentException($"User with id {report.UserId} not found");
                }

                var createdReport = await _serviceReportRepository.CreateServiceReportAsync(request.LineId,
                    report.UserId,
                    report.RequestId,
                    DateTime.Now.ToUniversalTime());

                _logger.Information("Service report created successfully with ID {ReportId}", createdReport.Id);

                await _serviceRequestRepository.UpdateServiceRequestAsync(createdReport.RequestId, new ServiceRequestUpdate(status: RequestStatusType.InProgress));

                _logger.Information("Request ID {RequestId} updated to InProgress", createdReport.RequestId);

                return createdReport;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create service report for request ID {RequestId}", report.RequestId);
                throw new ReportServiceException("Failed to create report", ex);
            }
        }

        public async Task<List<ServiceReport>> GetAllServiceReportsAsync(ServiceReportFilter filter)
        {
            _logger.Information("Attempting to retrieve all service reports with the specified filter");
            try
            {
                var reports = await _serviceReportRepository.GetAllServiceReportsAsync(filter);
                _logger.Information("Successfully retrieved {ReportCount} service reports", reports.Count);
                return reports;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve service reports");
                throw new ReportServiceException("Failed to get reports", ex);
            }
        }

        public async Task<ServiceReport> GetServiceReportByIdAsync(Guid id)
        {
            _logger.Information("Attempting to retrieve service report with ID {ReportId}", id);
            try
            {
                var report = await _serviceReportRepository.GetServiceReportByIdAsync(id);
                if (report == null)
                {
                    _logger.Warning("Service report with ID {ReportId} not found", id);
                    throw new ReportNotFoundException($"Report with id {id} not found");
                }
                _logger.Information("Successfully retrieved service report with ID {ReportId}", id);
                return report;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve service report with ID {ReportId}", id);
                throw new ReportServiceException("Failed to get report", ex);
            }
        }

        public async Task<ServiceReport> CloseServiceReportAsync(Guid id, ServiceReportClose report)
        {
            _logger.Information("Attempting to close service report with ID {ReportId}", id);
            try
            {
                var closedReport = await _serviceReportRepository.CloseServiceReportAsync(id,
                    DateTime.Now.ToUniversalTime(),
                    report.Price,
                    report.Description);

                _logger.Information("Service report with ID {ReportId} closed successfully", id);

                var closedRequest = await _serviceRequestRepository.UpdateServiceRequestAsync(closedReport.RequestId, new ServiceRequestUpdate(status: RequestStatusType.Closed));
                
                _logger.Information("Request ID {RequestId} updated to Closed", closedReport.RequestId);

                var additionalDownTime = (int)(closedReport.CloseDate.Value - closedRequest.RequestDate).TotalHours;
                var assemblyLine = await _assemblyLineRepository.GetAssemblyLineByIdAsync(closedReport.LineId);
                var newDownTime = assemblyLine.DownTime + additionalDownTime;

                if (closedRequest.Type == RequestType.Inspection)
                {
                    var newLastInspectionDate = closedReport.CloseDate.Value.Date;
                    var newNextInspectionDate = newLastInspectionDate.AddMonths(12 / assemblyLine.InspectionsPerYear);

                    await _assemblyLineRepository.UpdateAssemblyLineAsync(
                        closedReport.LineId,
                        new AssemblyLineUpdate(
                            status: LineStatusType.Working,
                            downTime: newDownTime,
                            lastInspection: DateOnly.FromDateTime(newLastInspectionDate),
                            nextInspection: DateOnly.FromDateTime(newNextInspectionDate)));

                    _logger.Information("Assembly line ID {LineId} updated with new inspection dates", closedReport.LineId);
                }
                else
                {
                    await _assemblyLineRepository.UpdateAssemblyLineAsync(closedReport.LineId,
                        new AssemblyLineUpdate(status: LineStatusType.Working,
                        downTime: newDownTime));

                    _logger.Information("Assembly line ID {LineId} updated with new down time", closedReport.LineId);
                }
                
                return closedReport;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to close service report with ID {ReportId}", id);
                throw new ReportServiceException("Failed to get report", ex);
            }
        }
    }
}
