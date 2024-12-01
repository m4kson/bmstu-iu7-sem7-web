using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailOrderRepositoryException : Exception
    {
        public DetailOrderRepositoryException()
        {
        }
        public DetailOrderRepositoryException(string? message) : base(message)
        {
        }

        public DetailOrderRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
