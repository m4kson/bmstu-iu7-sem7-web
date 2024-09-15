using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Application.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IDetailRepository _detailRepository;
        private readonly ISupplyRepository _supplyRepository;
        private readonly ILogger _logger;

        public SupplyService(IDetailRepository detailRepository, 
            ISupplyRepository supplyRepository,
            ILogger logger)
        {
            _detailRepository = detailRepository;
            _supplyRepository = supplyRepository;
            _logger = logger;
        }

        public async Task<Supply> CreateSupplyAsync(SupplyCreate supplyCreate)
        {
            _logger.Information("Attempting to create a new supply");
            try
            {
                var detail = await _detailRepository.GetDetailByIdAsync(supplyCreate.DetailId);
                if (detail == null)
                    throw new Exception("Detail not found");

                detail.Amount += supplyCreate.Quantity;
                await _detailRepository.UpdateDetailAsync(detail.Id, detail.Amount);


                var newSupply = await _supplyRepository.CreateSupplyAsync(supplyCreate);

                _logger.Information("Successfully created a new supply with ID {SupplyId}", newSupply.Id);
                return newSupply;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create supply", ex);
            }
            
        }
    }
}
