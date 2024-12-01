using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Requests;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;

[ApiController]
[Route("api/v1/ServiceRequests")]
public class ServiceRequestsController(IServiceRequestService serviceRequestService,
    ILogger logger): ControllerBase
{
    private readonly IServiceRequestService _serviceRequestService = serviceRequestService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RequestDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateServiceRequest([FromBody] RequestCreateDto serviceRequestCreateDto)
    {
        try
        {
            var serviceRequest = serviceRequestCreateDto.ToDomain();
            var createdServiceRequest = await _serviceRequestService.CreateServiceRequestAsync(serviceRequest);
            var response = createdServiceRequest.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(ServiceRequestsController)} : {nameof(CreateServiceRequest)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceRequestsController)} : {nameof(CreateServiceRequest)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<RequestDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllServiceRequests([FromQuery] RequestFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var serviceRequests = await _serviceRequestService.GetAllServiceRequestsAsync(filter);
            var response = serviceRequests.Select(s => s.ToDto()).ToList();
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceRequestsController)} : {nameof(GetAllServiceRequests)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RequestDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetServiceRequestById([FromRoute, Required] Guid id)
    {
        try
        {
            var serviceRequest = await _serviceRequestService.GetServiceRequestByIdAsync(id);
            var response = serviceRequest.ToDto();
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(ServiceRequestsController)} : {nameof(GetServiceRequestById)} : {e.Message}");
            throw;
        }
    }
}