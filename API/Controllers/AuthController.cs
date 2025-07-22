using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;    

namespace API.Controllers;

public class AuthController : BaseAPIController
{
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
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
            var token = GenerateToken(user);
            return Ok(token);
        }
    }

    [HttpGet("token")]
    public ActionResult<string> GetToken()
    {
        var user = context.Users.FirstOrDefault(u => u.Email == "test@test.com");
        return GenerateToken(user!);
    }                                   
    
    private string GenerateToken(User user) {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
  