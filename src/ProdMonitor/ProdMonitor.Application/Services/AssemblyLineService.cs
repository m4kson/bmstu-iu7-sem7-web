using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using System.Net.Http.Headers;
using Serilog;

namespace ProdMonitor.Application.Services
{
    public class AssemblyLineService : IAssemblyLineService
    {
        private readonly IAssemblyLineRepository _assemblyLineRepository;
        private readonly ILogger _logger;

        public AssemblyLineService(IAssemblyLineRepository assemblyLineRepository,
            ILogger logger)
        {
            _assemblyLineRepository = assemblyLineRepository;
            _logger = logger;
        }

        public async Task<AssemblyLine> CreateAssemblyLineAsync(AssemblyLineCreate line)
        {
            try
            {
                _logger.Information("Creating assembly line: {LineName}", line.Name);
                return await _assemblyLineRepository.CreateAssemblyLineAsync(line);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create assembly line: {LineName}", line.Name);
                throw new AssemblyLineServiceException("Failed to create line", ex);
            }
        }

        public async Task<List<AssemblyLine>> GetAllAssemblyLinesAsync(AssemblyLineFilter filter)
        {
            try
            {
                _logger.Information("Getting all assembly lines with filter: {Filter}", filter);
                var result = await _assemblyLineRepository.GetAllAssemblyLinesAsync(filter);
                if (!result.Any())
                {
                    _logger.Warning("No assembly lines found with filter: {Filter}", filter);
                    throw new LineNotFoundException("No lines found");
                }

                return result;
            }
            catch (LineNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get assembly lines");
                throw new AssemblyLineServiceException("Failed to get lines", ex);
            }
        }

        public async Task<AssemblyLine> GetAssemblyLineByIdAsync(Guid id)
        {
            try
            {
                _logger.Information("Getting assembly line with id: {Id}", id);
                var assemblyLine = await _assemblyLineRepository.GetAssemblyLineByIdAsync(id);
                if (assemblyLine == null)
                {
                    _logger.Warning("Assembly line with id {Id} not found", id);
                    throw new LineNotFoundException($"Line with id {id} not found");
                }

                return assemblyLine;
            }
            catch (LineNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get assembly line with id: {Id}", id);
                throw new AssemblyLineServiceException("Failed to get line", ex);
            }
        }

        public Task DeleteAssemblyLineAsync(Guid id)
        {
            try
            {
                _logger.Information("Deleting assembly line with id: {Id}", id);
                return _assemblyLineRepository.DeleteAssemblyLineAsync(id);
            }
            catch (LineNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete assembly line with id: {Id}", id);
                throw new AssemblyLineServiceException("Failed to delete line", ex);
            }
        }
    }
}
