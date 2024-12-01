using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using Serilog;

namespace ProdMonitor.Application.Services
{
    public class TractorService : ITractorService
    {
        private readonly ITractorRepository _tractorRepository;
        private readonly ILogger _logger;

        public TractorService(ITractorRepository tractorRepository,
            ILogger logger)
        {
            _tractorRepository = tractorRepository;
            _logger = logger;
        }

        public async Task<Tractor> CreateTractorAsync(TractorCreate tractor)
        {
            _logger.Information("Attempting to create a new tractor: {TractorName}", tractor.Model);
            try
            {
                var createdTractor = await _tractorRepository.CreateTractorAsync(tractor);
                _logger.Information("Successfully created tractor with ID {TractorId}", createdTractor.Id);
                return createdTractor;
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Failed to create tractor: {TractorName}", tractor.Model);
                throw new TractorServiceException("Failed to create tractor", ex);
            }

        }

        public async Task<List<Tractor>> GetAllTractorsAsync(TractorFilter filter)
        {
            _logger.Information("Attempting to retrieve all tractors with the specified filter");
            try
            {
                var tractors = await _tractorRepository.GetAllTractorsAsync(filter);
                if (!tractors.Any())
                {
                    _logger.Warning("No tractors found with the specified filter");
                    throw new TractorNotFoundException("No tractors found with the specified filter");
                }
                _logger.Information("Successfully retrieved {TractorCount} tractors", tractors.Count);
                return tractors;
            }
            catch (TractorNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve tractors");
                throw new TractorServiceException("Failed to get tractors", ex);
            }
        }

        public async Task<Tractor> GetTractorByIdAsync(Guid id)
        {
            _logger.Information("Attempting to retrieve tractor with ID {TractorId}", id);
            try
            {
                var tractor = await _tractorRepository.GetTractorByIdAsync(id);
                if (tractor == null)
                {
                    _logger.Warning("Tractor with ID {TractorId} not found", id);
                    throw new TractorNotFoundException($"Tractor with id {id} not found");
                }
                _logger.Information("Successfully retrieved tractor with ID {TractorId}", id);
                return tractor;
            }
            catch (TractorNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve tractor with ID {TractorId}", id);
                throw new TractorServiceException("Failed to get tractor", ex);
            }
        }

        public async Task DeleteTractorAsync(Guid id)
        {
            try
            {
                _logger.Information("Attempting to delete tractor with ID {TractorId}", id);
                await _tractorRepository.DeleteTractorAsync(id);
            }
            catch (TractorNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete tractor with ID {TractorId}", id);
                throw new TractorServiceException("Failed to delete tractor", ex);
            }
        }
    }
}
