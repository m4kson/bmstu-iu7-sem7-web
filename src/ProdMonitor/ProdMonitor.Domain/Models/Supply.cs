using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Models
{
    public class Supply
    {
        public Guid Id { get; set; }
        public Guid DetailId { get; set; }
        public DateTime SupplyDate { get; set; }
        public int Quantity { get; set; }

        public Supply(Guid id,
            Guid detailId,
            DateTime supplyDate,
            int quantity) 
        {
            Id = id; 
            DetailId = detailId;
            SupplyDate = supplyDate;
            Quantity = quantity;
        }
    }
}
