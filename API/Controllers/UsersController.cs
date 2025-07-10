using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Users.Queries;
using Application.Users.Commands;

namespace API.Controllers;


public class UsersController: BaseAPIController
{


    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await Mediator.Send(new GetUserList.Query());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserDetails(string id)
    {
        return await Mediator.Send(new GetUserDetail.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateUser(User user)
    {
        return await Mediator.Send(new CreateUser.Command { User = user });
    }

    [HttpPut]
    public async Task<ActionResult> EditUser(User user)
    {
        await Mediator.Send(new EditUser.Command { User = user });
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        await Mediator.Send(new DeleteUser.Command { Id = id });
        return Ok();
    }
}
