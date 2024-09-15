using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class TractorNotFoundException : Exception
    {
        public TractorNotFoundException()
        {
        }
        public TractorNotFoundException(string? message) : base(message)
        {
        }

        public TractorNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
