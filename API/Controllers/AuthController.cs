using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;

using Application.Services;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers;

public class AuthController(IAuthService authService) : BaseAPIController
{
    
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user = await authService.RegisterAsync(request);

        if(user is null) return BadRequest("User already exists");
   
        return Ok(user);        
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(LoginDto request)
    {
        var response = await authService.LoginAsync(request);

        if(response is null) return BadRequest("Invalid credentials");

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Authenticated()
    {
        return Ok("Authenticated");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("Admin only");
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDTO request)
    {
        var response = await authService.RefreshTokenAsync(request);
        if(response is null || response.AccessToken is null || response.RefreshToken is null) return Unauthorized("Invalid refresh token");
        return Ok(response);
    }
    
}
  