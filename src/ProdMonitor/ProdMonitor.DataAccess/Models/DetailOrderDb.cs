using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProdMonitor.DataAccess.Models.Enums;

namespace ProdMonitor.DataAccess.Models
{
    public class DetailOrderDb
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDb? User { get; set; } 
        public DetailOrderStatusDb Status { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetailDb> OrderDetails { get; set; }
    
    
        public DetailOrderDb(Guid id, 
            Guid userId,  
            DetailOrderStatusDb status, 
            float totalPrice, 
            DateTime orderDate)
        {
            Id = id;
            UserId = userId;
            Status = status;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
        }
    }
}
