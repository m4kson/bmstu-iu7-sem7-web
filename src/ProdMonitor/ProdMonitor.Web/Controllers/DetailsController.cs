using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Details;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;


[ApiController]
[Route("api/v1/Details")]
public class DetailsController(IDetailService detailService,
    ILogger logger): ControllerBase
{
    private readonly IDetailService _detailService = detailService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DetailDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDetail([FromBody] DetailCreateDto detailCreateDto)
    {
        try
        {
            var detail = detailCreateDto.ToDomain();
            var createdDetail = await _detailService.CreateDetailAsync(detail);
            var response = createdDetail.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(CreateDetail)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(CreateDetail)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<DetailDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDetails([FromQuery] DetailFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var details = await _detailService.GetAllDetailsAsync(filter);
            var response = details.Select(d => d.ToDto()).ToList();
            return Ok(response);
        }
        catch (DetailNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(GetAllDetails)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(GetAllDetails)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DetailDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetailById([FromRoute, Required] Guid id)
    {
        try
        {
            var detail = await _detailService.GetDetailByIdAsync(id);
            var response = detail.ToDto();
            return Ok(response);
        }
        catch (DetailNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(GetDetailById)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(GetDetailById)} : {e.Message}");
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status401Unauthorized)]
    [ProducesProblems(StatusCodes.Status403Forbidden)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDetail([FromRoute, Required] Guid id)
    {
        try
        {
            await _detailService.DeleteDetailAsync(id);
            return Ok();
        }
        catch (DetailNotFoundException e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(DeleteDetail)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(DetailsController)} : {nameof(DeleteDetail)} : {e.Message}");
            throw;
        }
    }
}