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
                _logger.Information("Successfully retrieved {TractorCount} tractors", tractors.Count);
                return tractors;
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve tractor with ID {TractorId}", id);
                throw new TractorServiceException("Failed to get tractor", ex);
            }
        }
    }
}
