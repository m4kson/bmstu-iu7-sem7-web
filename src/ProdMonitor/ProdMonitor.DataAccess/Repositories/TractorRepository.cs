using ProdMonitor.DataAccess.Context;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Converters;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.Domain.Exceptions;

namespace ProdMonitor.DataAccess.Repositories
{
    public class TractorRepository : ITractorRepository
    {
        private readonly ProdMonitorContext _context;

        public TractorRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<Tractor> CreateTractorAsync(TractorCreate tractor)
        {
            try
            {

                var newTractor = new TractorDb
                {
                    Id = Guid.NewGuid(),
                    Model = tractor.Model,
                    ReleaseYear = tractor.ReleaseYear,
                    EngineType = tractor.EngineType,
                    EnginePower = tractor.EnginePower,
                    FrontTireSize = tractor.frontTireSize,
                    BackTireSize = tractor.backTireSize,
                    WheelsAmount = tractor.wheelsAmount,
                    TankCapacity = tractor.tankCapacity,
                    EcologicalStandard = tractor.ecologicalStandart,
                    Length = tractor.length,
                    Width = tractor.width,
                    CabinHeight = tractor.cabinHeight,
                    AssemblyLines = tractor.assemblyLines?.Select(AssemblyLineConverter.ToDb).ToList() ?? new List<AssemblyLineDb>()
                };



                _context.Tractors.Add(newTractor);
                await _context.SaveChangesAsync();

                var createdTractor = await _context.Tractors
                    .Include(t => t.AssemblyLines)
                    .FirstOrDefaultAsync(t => t.Id == newTractor.Id);

                if (createdTractor == null)
                {
                    throw new InvalidOperationException("Tractor was not found after creation.");
                }

                return TractorConverter.ToDomain(createdTractor)!;
            }
            catch (Exception ex)
            {
                throw new TractorRepositoryException("Failed to create tractor", ex);
            }
        }

        public async Task<List<Tractor>> GetAllTractorsAsync(TractorFilter filter)
        {
            try
            {
                IQueryable<TractorDb> query = _context.Tractors;

                if (filter.ReleaseYear.HasValue)
                {
                    query = query.Where(t => t.ReleaseYear == filter.ReleaseYear.Value);
                }

                if (!string.IsNullOrEmpty(filter.EngineType))
                {
                    query = query.Where(t => t.EngineType == filter.EngineType);
                }

                if (!string.IsNullOrEmpty(filter.EcologicalStandart))
                {
                    query = query.Where(t => t.EcologicalStandard == filter.EcologicalStandart);
                }

                var tractors = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();

                return tractors.ConvertAll(t => TractorConverter.ToDomain(t)!);
            }
            catch (Exception ex)
            {
                throw new TractorRepositoryException("Failed to retrieve tractors", ex);
            }
        }

        public async Task<Tractor?> GetTractorByIdAsync(Guid id)
        {
            try
            {
                var tractorDb = await _context.Tractors
                    .AsNoTracking()
                    .Include(t => t.AssemblyLines)
                    .FirstOrDefaultAsync(t => t.Id == id);

                return TractorConverter.ToDomain(tractorDb);
            }
            catch (Exception ex)
            {
                throw new TractorRepositoryException("Failed to retrieve tractor", ex);
            }
        }
    }
}
