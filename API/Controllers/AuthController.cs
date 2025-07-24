using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Persistence;
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
    public async Task<ActionResult<string?>> Login(UserDto request)
    {
        var token = await authService.LoginAsync(request);

        if(token is null) return BadRequest("Invalid credentials");

        return Ok(token);
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
    
    
}
  