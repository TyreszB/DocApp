using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext context;

    public AuthController(AppDbContext context)
    {
        this.context = context;
    }
    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            PasswordHash = request.Password,
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsEmailVerified = false,
            IsPhoneVerified = false,
            IsActive = true,
            IsArchived = false,
        };

        var passwordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        user.Password = passwordHash;

        return Ok(user);        
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserDto request)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == request.Email);

        if(user?.Email != request.Email)
        {
            return BadRequest("User not found");
        }

        if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            return BadRequest("Invalid password");
        }
        else
        {
            var token = "success";
            return Ok(token);
        }
        
    }

 
}