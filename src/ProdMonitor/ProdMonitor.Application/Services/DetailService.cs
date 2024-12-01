using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using Serilog;

namespace ProdMonitor.Application.Services
{
    public class DetailService : IDetailService
    {
        private readonly IDetailRepository _detailRepository;
        private readonly ILogger _logger;
        private IDetailService _detailServiceImplementation;

        public DetailService(IDetailRepository detailRepository,
            ILogger logger)
        {
            _detailRepository = detailRepository;
            _logger = logger;
        }

        public async Task<Detail> CreateDetailAsync(DetailCreate detail)
        {
            _logger.Information("Attempting to create a new detail with name {DetailName}", detail.Name);
            try
            {
                var createdDetail = await _detailRepository.CreateDetailAsync(detail);
                _logger.Information("Successfully created a new detail with ID {DetailId}", createdDetail.Id);
                return createdDetail;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create detail with name {DetailName}", detail.Name);
                throw new DetailServiceException("Failed to create detail", ex);
            }
        }

        public async Task<List<Detail>> GetAllDetailsAsync(DetailFilter filter)
        {
            _logger.Information("Attempting to retrieve all details with the specified filter");

            try
            {
                var details = await _detailRepository.GetAllDetailsAsync(filter);
                if (!details.Any())
                {
                    _logger.Warning("No details found");
                    throw new DetailNotFoundException("No details found");
                }

                _logger.Information("Successfully retrieved {DetailCount} details", details.Count);
                return details;
            }
            catch (DetailNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve details");
                throw new DetailServiceException("Failed to get details", ex);
            }
        }

        public async Task<Detail> GetDetailByIdAsync(Guid id)
        {
            _logger.Information("Attempting to retrieve detail with ID {DetailId}", id);
            try
            {
                var detail = await _detailRepository.GetDetailByIdAsync(id);
                if (detail == null)
                {
                    _logger.Warning("Detail with ID {DetailId} not found", id);
                    throw new DetailNotFoundException($"Detail with id {id} not found");
                }

                _logger.Information("Successfully retrieved detail with ID {DetailId}", id);
                return detail;
            }
            catch (DetailNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve detail with ID {DetailId}", id);
                throw new DetailServiceException("Failed to get detail", ex);
            }
        }

        public Task DeleteDetailAsync(Guid id)
        {
            _logger.Information("Attempting to delete detail with ID {DetailId}", id);
            try
            {
                return _detailRepository.DeleteDetailAsync(id);
            }
            catch (DetailNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete detail with ID {DetailId}", id);
                throw new DetailServiceException("Failed to delete detail", ex);
            }
        }
    }
}
