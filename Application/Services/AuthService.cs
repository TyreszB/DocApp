using Application.Services;
using Application.Users.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Services;

public class AuthService(AppDbContext context) : IAuthService
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

    public async Task<string?> LoginAsync(UserDto request)
    {
        if(context.Users.Any(u => u.Email != request.Email)) return null;

        var user = context.Users.FirstOrDefault(u => u.Email == request.Email)
        ;
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user!, user!.PasswordHash, request.Password);
        
      
        
        if(result == PasswordVerificationResult.Failed)
        {
            return null;
        }
        else
        {
            var token = GenerateToken(user);
            return token;
        }
    }

    public string GenerateToken(User user)
    {
        throw new NotImplementedException();
    }

    public bool VerifyPassword(User user, string password)
    {
        throw new NotImplementedException();
    }
}
