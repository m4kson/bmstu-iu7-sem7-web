using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Orders;
using Serilog.Extensions;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;

[ApiController]
[Route("api/v1/DetailOrders")]
public class DetailOrdersController(IDetailOrderService detailOrderService,
    ILogger logger) : ControllerBase
{
    private readonly IDetailOrderService _detailOrderService = detailOrderService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDetailOrder([FromBody] OrderCreateDto detailOrderCreateDto)
    {
        try
        {
            var detailOrder = detailOrderCreateDto.ToDomain();
            var createdDetailOrder = await _detailOrderService.CreateDetailOrderAsync(detailOrder);
            var response = createdDetailOrder.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(CreateDetailOrder)} : {e.Message}");
            return BadRequest();
        }
        catch (DetailOrderNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(CreateDetailOrder)} : {e.Message}");
            return NotFound();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(CreateDetailOrder)} : {e.Message}");
            throw;
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDetailOrders([FromQuery] OrderFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var detailOrders = await _detailOrderService.GetAllDetailOrdersAsync(filter);
            var response = detailOrders.Select(d => d.ToDto()).ToList();
            return Ok(response);
        }
        catch (DetailOrderNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(GetAllDetailOrders)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(GetAllDetailOrders)} : {e.Message}");
            throw;
        }
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetailOrderById([FromRoute, Required] Guid id)
    {
        try
        {
            var detailOrder = await _detailOrderService.GetDetailOrderById(id);
            var response = detailOrder.ToDto();
            return Ok(response);
        }
        catch (DetailOrderNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(GetDetailOrderById)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailOrdersController)} : {nameof(GetDetailOrderById)} : {e.Message}");
            throw;
        }
    }
}