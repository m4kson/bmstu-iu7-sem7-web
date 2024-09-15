using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailRepositoryException : Exception
    {
        public DetailRepositoryException()
        {
        }
        public DetailRepositoryException(string? message) : base(message)
        {
        }

        public DetailRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
