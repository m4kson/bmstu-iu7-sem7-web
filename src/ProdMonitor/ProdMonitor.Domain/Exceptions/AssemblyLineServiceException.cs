using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class AssemblyLineServiceException : Exception
    {
        public AssemblyLineServiceException()
        {
        }
        public AssemblyLineServiceException(string? message) : base(message)
        {
        }

        public AssemblyLineServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
