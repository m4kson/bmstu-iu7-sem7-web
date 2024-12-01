using Microsoft.AspNetCore.Mvc;

namespace ProdMonitor.Web.Controllers.Helpers;

#nullable disable
public class ProducesProblemsAttribute : ProducesResponseTypeAttribute
{
    public ProducesProblemsAttribute(int statusCode)
        : base(typeof (ProblemDetails), statusCode, "application/problem+json")
    {
    }
}