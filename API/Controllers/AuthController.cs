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
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            PasswordHash = string.Empty,
            Role = "User"
        };

        var passwordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.PasswordHash = passwordHash;

        context.Users.Add(user);
        await context.SaveChangesAsync();

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

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        // Debug output
        Console.WriteLine($"User found: {user.Email}");
        Console.WriteLine($"Stored PasswordHash: {user.PasswordHash}");
        Console.WriteLine($"Attempting to verify password: {request.Password}");
        Console.WriteLine($"Verification result: {result}");
        
        if(result == PasswordVerificationResult.Failed)
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