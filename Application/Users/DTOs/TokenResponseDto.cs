using System.Text.Json.Serialization;

namespace Application.Users.DTOs;

public class TokenResponseDto
{
    [JsonPropertyName("userId")]
    public required string UserId { get; set; }
    
    [JsonPropertyName("token")]
    public required string AccessToken { get; set; }
    
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
} 