using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Users.Commands;
using Application.Users.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseAPIController
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        return await Mediator.Send(new LoginUser.Command { LoginDto = loginDto });
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        return await Mediator.Send(new RegisterUser.Command { RegisterDto = registerDto });
    }
} 