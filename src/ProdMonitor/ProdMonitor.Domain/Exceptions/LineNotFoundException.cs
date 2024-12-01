using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class LineNotFoundException : Exception
    {
        public LineNotFoundException()
        {
        }
        public LineNotFoundException(string? message) : base(message)
        {
        }

        public LineNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
