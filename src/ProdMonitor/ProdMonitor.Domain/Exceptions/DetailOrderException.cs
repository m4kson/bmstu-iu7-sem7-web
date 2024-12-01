using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailOrderException : Exception
    {
        public DetailOrderException()
        {
        }
        public DetailOrderException(string? message) : base(message)
        {
        }

        public DetailOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
