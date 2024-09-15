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
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly ProdMonitorContext _context;

        public ServiceRequestRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<ServiceRequest> CreateServiceRequestAsync(Guid lineId, 
            Guid userId, 
            DateTime requestDate, 
            RequestStatusType status, 
            RequestType type, 
            string description)
        {
            try
            {
                var serviceRequest = new ServiceRequest(
                    id: Guid.NewGuid(),
                    lineId: lineId,
                    userId: userId,
                    requestDate: requestDate,
                    status: status,
                    type: type,
                    description: description
                );

                var serviceRequestDb = ServiceRequestConverter.ToDb(serviceRequest);

                var result = _context.ServiceRequests.Add(serviceRequestDb!);
                await _context.SaveChangesAsync();

                var createdRequest = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == result.Entity.Id);

                if (createdRequest == null)
                {
                    throw new InvalidOperationException("Service request was not found after creation.");
                }

                return ServiceRequestConverter.ToDomain(createdRequest)!;
            }
            catch (Exception ex)
            {
                throw new ServiceRequestRepositoryException("Failed to create service request", ex);
            }
        }

        public async Task DeleteServiceRequest(Guid id)
        {
            try
            {
                var serviceRequest = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                if (serviceRequest == null)
                {
                    throw new KeyNotFoundException("Service request not found.");
                }

                _context.ServiceRequests.Remove(serviceRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServiceRequestRepositoryException("Failed to delete service request", ex);
            }
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync(ServiceRequestFilter filter)
        {
            try
            {
                IQueryable<ServiceRequestDb> query = _context.ServiceRequests;

                if (filter.LineId.HasValue)
                {
                    query = query.Where(sr => sr.LineId == filter.LineId.Value);
                }

                if (filter.UserId.HasValue)
                {
                    query = query.Where(sr => sr.UserId == filter.UserId.Value);
                }

                if (filter.Type.HasValue)
                {
                    query = query.Where(sr => sr.Type == RequestTypeConverter.ToDb(filter.Type.Value));
                }

                if (filter.Status.HasValue)
                {
                    query = query.Where(sr => sr.Status == RequestStatusTypeConverter.ToDb(filter.Status.Value));
                }

                if (filter.SortByDate.HasValue && filter.SortByDate.Value)
                {
                    query = query.OrderBy(sr => sr.RequestDate);
                }

                var serviceRequestsDb = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();

                return serviceRequestsDb.ConvertAll(sr => ServiceRequestConverter.ToDomain(sr)!);
            }
            catch (Exception ex)
            {
                throw new ServiceRequestRepositoryException("Failed to retrieve service requests", ex);
            }
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(Guid id)
        {
            try
            {
                var serviceRequestDb = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                return ServiceRequestConverter.ToDomain(serviceRequestDb);
            }
            catch (Exception ex)
            {
                throw new ServiceRequestRepositoryException("Failed to retrieve service request", ex);
            }
        }

        public async Task<ServiceRequest> UpdateServiceRequestAsync(Guid id, ServiceRequestUpdate updateData)
        {
            try
            {
                var serviceRequestDb = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                if (serviceRequestDb == null)
                {
                    throw new KeyNotFoundException("Service request not found.");
                }

                if (updateData.LineId.HasValue)
                {
                    serviceRequestDb.LineId = updateData.LineId.Value;
                }

                if (updateData.Type.HasValue)
                {
                    serviceRequestDb.Type = RequestTypeConverter.ToDb(updateData.Type.Value);
                }

                if (updateData.Status.HasValue)
                {
                    serviceRequestDb.Status = RequestStatusTypeConverter.ToDb(updateData.Status.Value);
                }

                if (!string.IsNullOrEmpty(updateData.Description))
                {
                    serviceRequestDb.Description = updateData.Description;
                }

                _context.ServiceRequests.Update(serviceRequestDb);
                await _context.SaveChangesAsync();

                return ServiceRequestConverter.ToDomain(serviceRequestDb)!;
            }
            catch (Exception ex)
            {
                throw new ServiceRequestRepositoryException("Failed to update service request", ex);
            }
        }
    }
}
