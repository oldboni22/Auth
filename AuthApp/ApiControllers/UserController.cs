using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Models;

namespace AuthApp.ApiControllers;

[ApiController]
[Route("user")]
public class UserController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpPost]
    public async Task<IActionResult> RegisterAsync( [FromBody] UserCreateDto dto)
    {
        await _serviceManager.User.CreateUserAsync(dto);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> LoginAsync( [FromBody] UserLoginDto dto)
    {
        var token = await _serviceManager.User.LoginAsync(dto);

        return Ok(token);
    }
    
}