
using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Application.Services;

public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
{
    public async Task<User?> RegisterAsync(UserDto request)
    {
        if(context.Users.Any(u => u.Email == request.Email))
        {
            return null;
        }

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

        return user;
    }

    public async Task<TokenResponseDto?> LoginAsync(UserDto request)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == request.Email);
        if(user is null) return null;
        
        var passwordHasher = new PasswordHasher<User>();

        var result = passwordHasher.VerifyHashedPassword(user!, user!.PasswordHash, request.Password);
        
      
        
        if(result == PasswordVerificationResult.Failed)
        {
            return null;
        }
        else
        {
            var response = new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
            return response; 
        }
    }

    public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDTO request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if(user is null) return null;
        var response = new TokenResponseDto
        {
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshToken(user)
        };
        return response;
    }

    private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
         var user = await context.Users.FindAsync(userId);
        if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }


    private string CreateRefreshToken() 
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshToken(User user) {
        var refreshToken = CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
        await context.SaveChangesAsync();
        return refreshToken;
       
    }

    private string CreateToken(User user) {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: configuration["AppSettings:Issuer"],
            audience: configuration["AppSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }


}
