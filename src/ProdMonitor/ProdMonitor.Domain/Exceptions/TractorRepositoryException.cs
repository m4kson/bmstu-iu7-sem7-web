using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class TractorRepositoryException : Exception
    {
        public TractorRepositoryException()
        {
        }
        public TractorRepositoryException(string? message) : base(message)
        {
        }

        public TractorRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
