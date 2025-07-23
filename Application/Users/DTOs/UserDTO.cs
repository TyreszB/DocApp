using System.Text.Json.Serialization;

namespace Application.Users.DTOs;

public class UserDto
{
 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [JsonPropertyName("password")]
    public required string Password { get; set; }
}