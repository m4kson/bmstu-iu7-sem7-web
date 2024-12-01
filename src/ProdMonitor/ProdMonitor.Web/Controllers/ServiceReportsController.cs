using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Reports;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;


[ApiController]
[Route("api/v1/ServiceReports")]
public class ServiceReportsController(IServiceReportService serviceReportService,
    ILogger logger): ControllerBase
{
    private readonly IServiceReportService _serviceReportService = serviceReportService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ReportDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateServiceReport([FromBody] ReportCreateDto serviceReportCreateDto)
    {
        try
        {
            var serviceReport = serviceReportCreateDto.ToDomain();
            var createdServiceReport = await _serviceReportService.CreateServiceReportAsync(serviceReport);
            var response = createdServiceReport.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(CreateServiceReport)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(CreateServiceReport)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ReportDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllServiceReports([FromQuery] ReportFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var serviceReports = await _serviceReportService.GetAllServiceReportsAsync(filter);
            var response = serviceReports.Select(s => s.ToDto()).ToList();
            return Ok(response);
        }
        catch (ReportNotFoundException e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(GetAllServiceReports)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(GetAllServiceReports)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ReportDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetServiceReportById([FromRoute, Required] Guid id)
    {
        try
        {
            var serviceReport = await _serviceReportService.GetServiceReportByIdAsync(id);
            var response = serviceReport.ToDto();
            return Ok(response);
        }
        
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(GetServiceReportById)} : {e.Message}");
            throw;
        }
    }
    
    [HttpPatch("close/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ReportDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CloseServiceReport([FromRoute, Required] Guid id, 
        [FromBody] ReportCloseDto serviceReportCloseDto)
    {
        try
        {
            var closeReport = serviceReportCloseDto.ToDomain();
            var serviceReport = await _serviceReportService.CloseServiceReportAsync(id, closeReport);
            var response = serviceReport.ToDto();
            return Ok(response);
        }
        catch (ReportNotFoundException e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(CloseServiceReport)} : {e.Message}");
            return NotFound();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceReportsController)} : {nameof(CloseServiceReport)} : {e.Message}");
            throw;
        }
    }
}