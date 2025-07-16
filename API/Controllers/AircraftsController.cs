using API.Controllers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Aircrafts.Commands;
using Application.Aircrafts.Queries;

namespace API.Controllers;

public class AircraftsController: BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<Aircraft>>> GetAircrafts()
    {
        return await Mediator.Send(new GetAircrafts.Query());
    }

    [HttpPost]
    public async Task<ActionResult<Aircraft>> CreateAircraft(Aircraft aircraft)
    {
        return await Mediator.Send(new CreateAircraft.Command { Aircraft = aircraft });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Aircraft>> DeleteAircraft(string id)
    {
        await Mediator.Send(new DeleteAircraft.Command { Id = id });
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAircraft(string id, Aircraft aircraft)
    {
        await Mediator.Send(new EditAircraft.Command { Id = id, Aircraft = aircraft });
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Aircraft>> GetAircraftDetails(string id)
    {
        return await Mediator.Send(new GetAircraftDetails.Query { Id = id });
    }
}   