using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models.Converters;
using ProdMonitor.DataAccess.Models.Converters.Enums;
using ProdMonitor.Domain.Exceptions;

namespace ProdMonitor.DataAccess.Repositories
{
    public class AssemblyLineRepository : IAssemblyLineRepository
    {
        private readonly ProdMonitorContext _context;

        public AssemblyLineRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<AssemblyLine> CreateAssemblyLineAsync(AssemblyLineCreate line)
        {
            try
            {
                var assemblyLine = new AssemblyLine(id: Guid.NewGuid(),
                name: line.Name,
                length: line.Length,
                height: line.Height,
                width: line.Width,
                status: line.Status,
                production: line.Production,
                downTime: line.DownTime,
                inspectionsPerYear: line.InspectionsPerYear,
                lastInspection: line.LastInspection,
                nextInspection: line.NextInspection,
                defectRate: line.DefectRate,
                tractors: line.Tractors,
                details: line.Details);

                var assemblyLineDb = AssemblyLineConverter.ToDb(assemblyLine);

                var result = _context.AssemblyLines.Add(assemblyLineDb!);
                await _context.SaveChangesAsync();

                var createdLine = await _context.AssemblyLines
                    .Include(l => l.Tractors)
                    .Include(l => l.Details)
                    .Include(l => l.ServiceReports)
                    .Include(l => l.ServiceRequests)
                    .FirstOrDefaultAsync(l => l.Id == result.Entity.Id);

                if (createdLine == null)
                {
                    throw new InvalidOperationException("Assembly line was not found after creation.");
                }

                return AssemblyLineConverter.ToDomain(createdLine)!;
            }
            catch (Exception ex)
            {
                throw new AssemblyLineRepositoryException("Failed to create line", ex);
            }
        }

        public async Task<List<AssemblyLine>> GetAllAssemblyLinesAsync(AssemblyLineFilter filter)
        {
            IQueryable<AssemblyLineDb> query = _context.AssemblyLines;

            if (filter.Status is not null)
                query = query.Where(l => l.Status == LineStatusTypeConverter.ToDb(filter.Status.Value));

            var assemblyLines = await query
                .Skip(filter.Skip)
                .Take(filter.Limit)
                .AsNoTracking()
                .ToListAsync();
            
            var result = assemblyLines.ConvertAll(l => AssemblyLineConverter.ToDomain(l)!);
            return result;
        }

        public async Task<AssemblyLine?> GetAssemblyLineByIdAsync(Guid id)
        {
            var assemblyLine = await _context.AssemblyLines
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
            return AssemblyLineConverter.ToDomain(assemblyLine);
        }

        public async Task<AssemblyLine> UpdateAssemblyLineAsync(Guid id, AssemblyLineUpdate assemblyLineUpdate)
        {
            try
            {
                var assemblyLine = await _context.AssemblyLines
                .FirstOrDefaultAsync(l => l.Id == id);

                if (assemblyLine == null)
                {
                    throw new KeyNotFoundException("Line not found.");
                }

                if (!string.IsNullOrEmpty(assemblyLineUpdate.Name))
                {
                    assemblyLine.Name = assemblyLineUpdate.Name;
                }

                if (assemblyLineUpdate.Status.HasValue)
                {
                    assemblyLine.Status = LineStatusTypeConverter.ToDb(assemblyLineUpdate.Status.Value);
                }

                if (assemblyLineUpdate.Production.HasValue)
                {
                    assemblyLine.Production = assemblyLineUpdate.Production.Value;
                }

                if (assemblyLineUpdate.DownTime.HasValue)
                {
                    assemblyLine.DownTime = assemblyLineUpdate.DownTime.Value;
                }

                if (assemblyLineUpdate.InspectionsPerYear.HasValue)
                {
                    assemblyLine.InspectionsPerYear = assemblyLineUpdate.InspectionsPerYear.Value;
                }

                if (assemblyLineUpdate.LastInspection.HasValue)
                {
                    assemblyLine.LastInspection = assemblyLineUpdate.LastInspection.Value;
                }

                if (assemblyLineUpdate.NextInspection.HasValue)
                {
                    assemblyLine.NextInspection = assemblyLineUpdate.NextInspection.Value;
                }

                if (assemblyLineUpdate.DefectRate.HasValue)
                {
                    assemblyLine.DefectRate = assemblyLineUpdate.DefectRate.Value;
                }

                _context.AssemblyLines.Update(assemblyLine);
                await _context.SaveChangesAsync();

                return AssemblyLineConverter.ToDomain(assemblyLine)!;
            }
            catch (Exception ex)
            {
                throw new AssemblyLineRepositoryException("Failed to update AssemblyLine", ex);
            }
        }
    }
}
