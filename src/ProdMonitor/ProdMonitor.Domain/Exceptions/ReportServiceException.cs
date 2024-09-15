using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.Domain.Exceptions
{
    public class ReportServiceException: Exception
    { 
        public ReportServiceException()
        {
        }
        public ReportServiceException(string? message) : base(message)
        {
        }

        public ReportServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
