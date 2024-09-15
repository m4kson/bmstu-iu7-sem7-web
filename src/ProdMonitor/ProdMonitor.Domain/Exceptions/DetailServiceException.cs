using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailServiceException : Exception
    {
        public DetailServiceException()
        {
        }
        public DetailServiceException(string? message) : base(message)
        {
        }

        public DetailServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
