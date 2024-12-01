using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Repositories
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly ProdMonitorContext _context;

        public SupplyRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<Supply> CreateSupplyAsync(SupplyCreate supplyCreate)
        {
            var supply = new SupplyDb(
               Guid.NewGuid(),
               supplyCreate.DetailId,
               DateTime.UtcNow, 
               supplyCreate.Quantity
           );

            try
            {
                await _context.Supplies.AddAsync(supply);

                await _context.SaveChangesAsync();

                return new Supply(
                    supply.Id,
                    supply.DetailId,
                    supply.SupplyDate,
                    supply.Quantity
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve supply", ex);
            }
            
        }
    }
}
