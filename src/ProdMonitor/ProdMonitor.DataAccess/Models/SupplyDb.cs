using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models
{
    public class SupplyDb
    {
        public Guid Id { get; set; }
        public Guid DetailId { get; set; }
        public DateTime SupplyDate { get; set; }
        public int Quantity { get; set; }

        public DetailDb? Detail { get; set; }

        public SupplyDb(Guid id,
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
