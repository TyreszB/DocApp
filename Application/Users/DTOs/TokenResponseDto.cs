using System.Text.Json.Serialization;

namespace Application.Users.DTOs;

public class TokenResponseDto
{
    [JsonPropertyName("token")]
    public required string AccessToken { get; set; }
    
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
    
} 