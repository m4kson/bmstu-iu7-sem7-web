using Microsoft.AspNetCore.Mvc;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Web.Controllers.Converters;
using ProdMonitor.Web.Controllers.Helpers;
using ProdMonitor.Web.Dto.Auth;
using ProdMonitor.Web.Dto.Users;
using Serilog.Extensions;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProdMonitor.Web.Controllers;


[ApiController]
[Route("api/v1/Auth")]
public class AuthController(IAuthenticationService authenticationService,
    ILogger logger): ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly ILogger _logger = logger;

    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status404NotFound)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData)
    {
        try
        {
            var user = await _authenticationService.LoginAsync(loginData.ToDomain());
            var response = user.ToDto();
            return Ok(response);
        }
        catch (WrongPasswordException e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Login)} : {e.Message}");
            return BadRequest();
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Login)} : {e.Message}");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Login)} : {e.Message}");
            throw;
        }
    }
    
    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerData)
    {
        try
        {
            var user = await _authenticationService.RegisterAsync(registerData.ToDomain());
            var response = user.ToDto();
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Register)} : {e.Message}");
            return BadRequest();
        }
        
        catch (UserAlreadyExistException e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Register)} : {e.Message}");
            return BadRequest();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Register)} : {e.Message}");
            throw;
        }
    }
    
    [HttpPost("logout")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesProblems(StatusCodes.Status400BadRequest)]
    [ProducesProblems(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.Error(e, $"{nameof(AuthController)} : {nameof(Logout)} : {e.Message}");
            throw;
        }
    }
}