using Application.Users.DTOs;
using Domain;

namespace Application.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(UserDto request);
    Task<string> LoginAsync(UserDto request);
    string GenerateToken(User user);
    bool VerifyPassword(User user, string password);
} 