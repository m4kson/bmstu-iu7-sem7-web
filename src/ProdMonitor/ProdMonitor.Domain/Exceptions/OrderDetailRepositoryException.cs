using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class OrderDetailRepositoryException : Exception
    {
        public OrderDetailRepositoryException()
        {
        }
        public OrderDetailRepositoryException(string? message) : base(message)
        {
        }

        public OrderDetailRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
