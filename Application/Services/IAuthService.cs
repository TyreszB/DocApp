using Application.Users.DTOs;
using Domain;

namespace Application.Services;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(LoginDto request);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDTO request);
} 