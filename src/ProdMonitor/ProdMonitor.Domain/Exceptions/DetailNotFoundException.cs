using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class DetailNotFoundException : Exception
    {
        public DetailNotFoundException()
        {
        }
        public DetailNotFoundException(string? message) : base(message)
        {
        }

        public DetailNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
