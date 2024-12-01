using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        Task<Supply> CreateSupplyAsync(SupplyCreate supplyCreate);
    }
}
