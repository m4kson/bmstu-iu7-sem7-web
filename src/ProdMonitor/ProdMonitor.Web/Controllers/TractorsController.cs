using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Tractors;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;


[ApiController]
[Route("api/v1/Tractors")]
public class TractorsController(ITractorService tractorService,
    ILogger logger): ControllerBase
{
    private readonly ITractorService _tractorService = tractorService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TractorDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTractor([FromBody] TractorCreateDto tractorCreateDto)
    {
        try
        {
            var tractor = tractorCreateDto.ToDomain();
            var createdTractor = await _tractorService.CreateTractorAsync(tractor);
            var response = createdTractor.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(CreateTractor)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(CreateTractor)} : {e.Message}");
            throw;
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TractorDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTractors([FromQuery] TractorFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var tractors = await _tractorService.GetAllTractorsAsync(filter);
            var response = tractors.Select(t => t.ToDto()).ToList();
            return Ok(response);
        }
        catch (TractorNotFoundException e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(GetAllTractors)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(GetAllTractors)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TractorDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTractorById([FromRoute] [Required] Guid id)
    {
        try
        {
            var tractor = await _tractorService.GetTractorByIdAsync(id);
            var response = tractor.ToDto();
            return Ok(response);
        }
        catch (TractorNotFoundException e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(GetTractorById)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(GetTractorById)} : {e.Message}");
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status401Unauthorized)]
    [ProducesProblems(StatusCodes.Status403Forbidden)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTractor([FromRoute] [Required] Guid id)
    {
        try
        {
            await _tractorService.DeleteTractorAsync(id);
            return Ok();
        }
        catch (TractorNotFoundException e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(DeleteTractor)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(TractorsController)} : {nameof(DeleteTractor)} : {e.Message}");
            throw;
        }
    }
}