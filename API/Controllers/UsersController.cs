using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;

namespace API.Controllers;


public class UsersController(AppDbContext context) : BaseAPIController
{


    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserDetails(string id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }
}
