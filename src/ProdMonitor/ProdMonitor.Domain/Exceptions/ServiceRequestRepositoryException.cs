using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class ServiceRequestRepositoryException : Exception
    {
        public ServiceRequestRepositoryException()
        {
        }
        public ServiceRequestRepositoryException(string? message) : base(message)
        {
        }

        public ServiceRequestRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
