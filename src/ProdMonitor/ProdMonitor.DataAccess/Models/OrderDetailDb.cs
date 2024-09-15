using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models
{
    public class OrderDetailDb
    {
        public Guid Id { get; set; }
        public Guid DetailId { get; set; }
        public Guid DetailOrderId { get; set; }
        public int DetailsAmount { get; set; }

        public virtual DetailDb Detail {  get; set; }
        public virtual DetailOrderDb Order { get; set; }

        public OrderDetailDb(Guid id,
            Guid detailId,
            Guid detailOrderId,
            int detailsAmount) 
        {
            Id = id;
            DetailId = detailId;
            DetailOrderId = detailOrderId;
            DetailsAmount = detailsAmount;
        }
    }
}
