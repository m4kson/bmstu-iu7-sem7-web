using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class RequestServiceException: Exception
    {
        public RequestServiceException()
        {
        }
        public RequestServiceException(string? message) : base(message)
        {
        }

        public RequestServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
