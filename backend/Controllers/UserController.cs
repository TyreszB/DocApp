using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static List<User> _users = new();

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        _users.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public ActionResult<User> Update(Guid id, User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.Position = user.Position;
        existingUser.IsActive = user.IsActive;
        existingUser.UpdatedAt = DateTime.UtcNow;

        return Ok(existingUser);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _users.Remove(user);
        return NoContent();
    }

 

    [HttpGet("active")]
    public ActionResult<IEnumerable<User>> GetActiveUsers()
    {
        var activeUsers = _users.Where(u => u.IsActive);
        return Ok(activeUsers);
    }

    [HttpPost("login")]
    public ActionResult<User> Login([FromBody] LoginRequest request)
    {
        var user = _users.FirstOrDefault(u => 
            u.Email == request.Email && 
            u.Password == request.Password && 
            u.IsActive);
        
        if (user == null)
            return Unauthorized();
        
        return Ok(user);
    }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
} 