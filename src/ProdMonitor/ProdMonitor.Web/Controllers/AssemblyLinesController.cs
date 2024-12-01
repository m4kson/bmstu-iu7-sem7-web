using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.AssemblyLines;
using Serilog.Extensions;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;

[ApiController]
[Route("api/v1/AssemblyLines")]
public class AssemblyLinesController(IAssemblyLineService assemblyLineService,
    ILogger logger): ControllerBase
{
    private readonly IAssemblyLineService _assemblyLineService = assemblyLineService;
    private readonly ILogger _logger = logger;
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AssemblyLineDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAssemblyLine([FromBody] AssemblyLineCreateDto assemblyLineCreateDto)
    {
        try
        {
            var assemblyLine = assemblyLineCreateDto.ToDomain();
            var createdAssemblyLine = await _assemblyLineService.CreateAssemblyLineAsync(assemblyLine);
            var response = createdAssemblyLine.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(CreateAssemblyLine)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(CreateAssemblyLine)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<AssemblyLineDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAssemblyLines([FromQuery] AssemblyLineFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var assemblyLines = await _assemblyLineService.GetAllAssemblyLinesAsync(filter);
            var response = assemblyLines.Select(a => a.ToDto()).ToList();
            return Ok(response);
        }
        catch (LineNotFoundException e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(GetAllAssemblyLines)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(GetAllAssemblyLines)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AssemblyLineDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAssemblyLineById([FromRoute, Required] Guid id)
    {
        try
        {
            var assemblyLine = await _assemblyLineService.GetAssemblyLineByIdAsync(id);
            var response = assemblyLine.ToDto();
            return Ok(response);
        }
        catch (LineNotFoundException e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(GetAssemblyLineById)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(GetAssemblyLineById)} : {e.Message}");
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AssemblyLineDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status401Unauthorized)]
    [ProducesProblems(StatusCodes.Status403Forbidden)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAssemblyLine([FromRoute, Required] Guid id)
    {
        try
        {
            await _assemblyLineService.DeleteAssemblyLineAsync(id);
            return Ok();
        }
        catch (LineNotFoundException e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(DeleteAssemblyLine)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AssemblyLinesController)} : {nameof(DeleteAssemblyLine)} : {e.Message}");
            throw;
        }
    }
}