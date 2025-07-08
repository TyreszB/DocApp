using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Users.Queries;

namespace API.Controllers;


public class UsersController(AppDbContext context, IMediator mediator) : BaseAPIController
{


    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await mediator.Send(new GetUserList.Query());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserDetails(string id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }
}
