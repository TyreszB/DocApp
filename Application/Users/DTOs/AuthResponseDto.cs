namespace Application.Users.DTOs;

public class AuthResponseDto
{
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? Message { get; set; }
    public DateTime? ExpiresAt { get; set; }
} 