using System.Text.Json.Serialization;

namespace Application.Users.DTOs;

public class LoginDto
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [JsonPropertyName("password")]
    public required string Password { get; set; }
} 