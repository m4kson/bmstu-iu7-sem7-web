using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models.Enums;
using Serilog;

namespace ProdMonitor.Application.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _requestRepository;
        private readonly IAssemblyLineRepository _assemblyLineRepository;
        private readonly ILogger _logger;


        public ServiceRequestService(IServiceRequestRepository repository,
            IAssemblyLineRepository lineRepository,
            ILogger logger)
        {
            _requestRepository = repository;
            _assemblyLineRepository = lineRepository;
            _logger = logger;
        }

        public async Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequestCreate serviceRequest)
        {
            _logger.Information("Attempting to create a service request for line ID {LineId}", serviceRequest.LineId);
            var line = await _assemblyLineRepository.GetAssemblyLineByIdAsync(serviceRequest.LineId);
            if (line == null)
            {
                _logger.Warning("Assembly line with ID {LineId} not found", serviceRequest.LineId);
                throw new RequestServiceException($"Failed to create request to line with id {serviceRequest.LineId}");
            }
            try
            {
                var createdReqest = await _requestRepository.CreateServiceRequestAsync(serviceRequest.LineId,
                    serviceRequest.UserId,
                    DateTime.Now.ToUniversalTime(),
                    RequestStatusType.Opened,
                    serviceRequest.Type,
                    serviceRequest.Description);

                await _assemblyLineRepository.UpdateAssemblyLineAsync(serviceRequest.LineId, new AssemblyLineUpdate(status: LineStatusType.OnService));
                _logger.Information("Service request created successfully for line ID {LineId} with request ID {RequestId}", serviceRequest.LineId, createdReqest.Id);
                return createdReqest;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create service request for line ID {LineId}", serviceRequest.LineId);
                throw new RequestServiceException("Failed to create request", ex);
            }
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync(ServiceRequestFilter filter)
        {
            _logger.Information("Attempting to retrieve all service requests with the specified filter");
            try
            {
                var requests = await _requestRepository.GetAllServiceRequestsAsync(filter);
                if (!requests.Any())
                {
                    _logger.Warning("No service requests found");
                    throw new RequestNotFoundException("No requests found");
                }

                _logger.Information("Successfully retrieved {RequestCount} service requests", requests.Count);
                return requests;
            }
            catch (RequestNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve service requests");
                throw new RequestServiceException("Failed to get requests", ex);
            }
        }

        public async Task<ServiceRequest> GetServiceRequestByIdAsync(Guid id)
        {
            _logger.Information("Attempting to retrieve service request with ID {RequestId}", id);

            try
            {
                var request = await _requestRepository.GetServiceRequestByIdAsync(id);
                if (request == null)
                {
                    _logger.Warning("Service request with ID {RequestId} not found", id);
                    throw new RequestNotFoundException($"Request with id {id} not found");
                }
                _logger.Information("Successfully retrieved service request with ID {RequestId}", id);
                return request;
            }
            catch (RequestNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve service request with ID {RequestId}", id);
                throw new RequestServiceException("Failed to get request", ex);
            }
        }
    }
}
