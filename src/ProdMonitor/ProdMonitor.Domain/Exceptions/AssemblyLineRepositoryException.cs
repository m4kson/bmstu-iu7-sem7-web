using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class AssemblyLineRepositoryException : Exception
    {
        public AssemblyLineRepositoryException()
        {
        }
        public AssemblyLineRepositoryException(string? message) : base(message)
        {
        }

        public AssemblyLineRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
