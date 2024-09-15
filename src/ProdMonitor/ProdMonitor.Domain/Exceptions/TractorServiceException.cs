using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class TractorServiceException : Exception
    {
        public TractorServiceException()
        {
        }
        public TractorServiceException(string? message) : base(message)
        {
        }

        public TractorServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
