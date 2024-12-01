using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface ISupplyRepository
    {
        Task<Supply> CreateSupplyAsync(SupplyCreate supplyCreate);
    }
}
