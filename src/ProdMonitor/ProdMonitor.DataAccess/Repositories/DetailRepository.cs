using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Converters;
using ProdMonitor.Domain.Exceptions;

namespace ProdMonitor.DataAccess.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        private readonly ProdMonitorContext _context;

        public DetailRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<Detail> CreateDetailAsync(DetailCreate detail)
        {
            try
            {
                var newDetail = new Detail(
                    id: Guid.NewGuid(),
                    name: detail.Name,
                    country: detail.Country,
                    amount: detail.Amount,
                    price: detail.Price,
                    length: detail.Length,
                    height: detail.Height,
                    width: detail.Width,
                    assemblyLines: detail.AssemblyLines
                );

                var detailDb = DetailConverter.ToDb(newDetail);

                var result = _context.Details.Add(detailDb!);
                await _context.SaveChangesAsync();

                var createdDetail = await _context.Details
                    .Include(d => d.AssemblyLines)
                    .Include(d => d.OrderDetails)
                    .FirstOrDefaultAsync(d => d.Id == result.Entity.Id);

                if (createdDetail == null)
                {
                    throw new InvalidOperationException("Detail was not found after creation.");
                }

                return DetailConverter.ToDomain(createdDetail)!;
            }
            catch (Exception ex)
            {
                throw new DetailRepositoryException("Failed to create detail", ex);
            }
        }

        public async Task<List<Detail>> GetAllDetailsAsync(DetailFilter filter)
        {
            try
            {
                IQueryable<DetailDb> query = _context.Details;

                if (!string.IsNullOrEmpty(filter.Country))
                {
                    query = query.Where(d => d.Country == filter.Country);
                }

                var details = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();


                var result = details.ConvertAll(d => DetailConverter.ToDomain(d)!);
                return result;
            }
            catch (Exception ex)
            {
                throw new DetailRepositoryException("Failed to retrieve details", ex);
            }
        }

        public async Task<Detail?> GetDetailByIdAsync(Guid id)
        {
            try
            {
                var detailDb = await _context.Details
                    .FirstOrDefaultAsync(d => d.Id == id);

                return DetailConverter.ToDomain(detailDb);
            }
            catch (Exception ex)
            {
                throw new DetailRepositoryException("Failed to retrieve detail by id", ex);
            }
        }

        public async Task UpdateDetailAsync(Guid id, int amaunt)
        {
            try
            {
                var newDetail = await _context.Details.FindAsync(id);
                newDetail.Amount = amaunt;
                _context.Details.Update(newDetail);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update detail", ex);
            }
            
        }
    }
}

