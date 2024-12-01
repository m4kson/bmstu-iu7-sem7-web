using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailOrderNotFoundException : Exception
    {
        public DetailOrderNotFoundException()
        {
        }
        public DetailOrderNotFoundException(string? message) : base(message)
        {
        }

        public DetailOrderNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
