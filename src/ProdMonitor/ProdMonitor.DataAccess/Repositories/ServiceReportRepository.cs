using ProdMonitor.DataAccess.Context;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Converters;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.DataAccess.Models.Converters.Enums;

namespace ProdMonitor.DataAccess.Repositories
{
    public class ServiceReportRepository : IServiceReportRepository
    {
        private readonly ProdMonitorContext _context;

        public ServiceReportRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<ServiceReport> CloseServiceReportAsync(Guid id, DateTime closeDate, float price, string description)
        {
            try
            {
                var serviceReportDb = await _context.ServiceReports.FirstOrDefaultAsync(sr => sr.Id == id);

                if (serviceReportDb == null)
                {
                    throw new ReportNotFoundException($"Service report with ID {id} not found.");
                }

                serviceReportDb.CloseDate = closeDate;
                serviceReportDb.Price = price;
                serviceReportDb.Description = description;

                await _context.SaveChangesAsync();

                return ServiceReportConverter.ToDomain(serviceReportDb)!;
            }
            catch (ReportNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceReportRepositoryException("Failed to close service report", ex);
            }
        }

        public async Task<ServiceReport> CreateServiceReportAsync(Guid lineId, Guid userId, Guid requestId, DateTime openDate)
        {
            try
            {
                var serviceReport = new ServiceReportDb
                {
                    Id = Guid.NewGuid(),
                    LineId = lineId,
                    UserId = userId,
                    RequestId = requestId,
                    OpenDate = openDate,
                };

                //var serviceReportDb = ServiceReportConverter.ToDb(serviceReport);

                var result = _context.ServiceReports.Add(serviceReport);
                await _context.SaveChangesAsync();

                var createdReport = await _context.ServiceReports
                    // .Include(s => s.ServiceRequest)
                    // .Include(s => s.User)
                    // .Include(s => s.AssemblyLine)
                    .FirstOrDefaultAsync(sr => sr.Id == result.Entity.Id);

                if (createdReport == null)
                {
                    throw new InvalidOperationException("Service report was not found after creation.");
                }

                return ServiceReportConverter.ToDomain(createdReport)!;
            }
            catch (Exception ex)
            {
                throw new ServiceReportRepositoryException("Failed to create service report", ex);
            }
        }


        public async Task<List<ServiceReport>> GetAllServiceReportsAsync(ServiceReportFilter filter)
        {
            try
            {
                IQueryable<ServiceReportDb> query = _context.ServiceReports;

                if (filter.LineId is not null)
                {
                    query = query.Where(sr => sr.LineId == filter.LineId.Value);
                }

                if (filter.UserId is not null)
                {
                    query = query.Where(sr => sr.UserId == filter.UserId.Value);
                }

                if (filter.RequestId is not null)
                {
                    query = query.Where(sr => sr.RequestId == filter.RequestId.Value);
                }

                if (filter.SortByDate is not null)
                {
                    query = query.OrderByDescending(sr => sr.OpenDate);
                }

                var serviceReportsDb = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();
                var serviceReports = serviceReportsDb.ConvertAll(sr => ServiceReportConverter.ToDomain(sr)!);

                return serviceReports;
            }
            catch (Exception ex)
            {
                throw new ServiceReportRepositoryException("Failed to retrieve service reports", ex);
            }
        }

        public async Task<ServiceReport?> GetServiceReportByIdAsync(Guid id)
        {
            try
            {
                var serviceReportDb = await _context.ServiceReports
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                return ServiceReportConverter.ToDomain(serviceReportDb);
            }
            catch (Exception ex)
            {
                throw new ServiceReportRepositoryException("Failed to retrieve service report", ex);
            }
        }
    }
}
