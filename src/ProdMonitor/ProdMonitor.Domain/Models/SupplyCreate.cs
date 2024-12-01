using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Models
{
    public class SupplyCreate(Guid detailId,
        int quantity)
    {
        public Guid DetailId { get; set; } = detailId;
        public int Quantity { get; set; } = quantity;
    }
}
