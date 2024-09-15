using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Enums
{
    public enum DetailOrderStatusDb
    {
        Processing = 0,
        InWork = 1,
        InDelivery = 2,
        Done = 3,
    }
}
