using BackendProjectManagement.DTOs;
using BackendProjectManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _authService.RegisterAsync(dto);
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
      
        var token = await _authService.LoginAsync(dto);

       
        var user = await _authService.GetByEmailAsync(dto.Email);
        if (user == null) return NotFound();

        
        return Ok(new
        {
            token,
            email = user.Email
        });
    }


    

}
