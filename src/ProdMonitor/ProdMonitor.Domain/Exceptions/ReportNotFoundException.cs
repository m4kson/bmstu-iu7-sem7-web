using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class ReportNotFoundException: Exception
    {
        public ReportNotFoundException()
        {
        }
        public ReportNotFoundException(string? message) : base(message)
        {
        }

        public ReportNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
