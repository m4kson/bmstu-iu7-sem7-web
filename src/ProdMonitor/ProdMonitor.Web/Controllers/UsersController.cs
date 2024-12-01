using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Converters.Enums;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Auth;
using ProdMonitor.Web.Dto.Enums;
using ProdMonitor.Web.Dto.Users;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;

[ApiController]
[Route("api/v1/Users")]
public class UsersController(IUserService userService,
    ILogger logger): ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ILogger _logger = logger;
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserFilterDto filterDto)
    {
        try
        {
            var filter = filterDto.ToDomain();
            var users = await _userService.GetAllUsersAsync(filter);
            var response = users.Select(u => u.ToDto()).ToList();
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(GetAllUsers)} : {e.Message}");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById([FromRoute, Required] Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            var response = user.ToDto();
            return Ok(response);
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(GetUserById)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(GetUserById)} : {e.Message}");
            throw;
        }
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser([FromRoute, Required] Guid id, [FromBody] RegisterDto userUpdateDto)
    {
        try
        {
            var userData = userUpdateDto.ToDomain();
            var updatedUser = await _userService.UpdateUser(id, userData);
            var response = updatedUser.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(UpdateUser)} : {e.Message}");
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(UpdateUser)} : {e.Message}");
            throw;
        }
    }
    
    [HttpPatch("UpdateRole/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserRole([FromRoute, Required] Guid id, [FromBody] UserUpdateRoleDto userRoleUpdateDto)
    {
        try
        {
            var role = userRoleUpdateDto.ToDomain();
            var updatedUser = await _userService.UpdateUserRole(id, role);
            var response = updatedUser.ToDto();
            return Ok(response);
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(UpdateUserRole)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(UsersController)} : {nameof(UpdateUserRole)} : {e.Message}");
            throw;
        }
    }
}