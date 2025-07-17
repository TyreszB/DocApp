using Microsoft.AspNetCore.Mvc;
using Application.Discrepencies.Queries;
using Domain;
using Application.Discrepencies.Commands;
namespace API.Controllers;

public class DiscrepenciesController : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<Discrepency>>> GetDiscrepencies()
    {
        return await Mediator.Send(new GetDiscrepencies.Query());
    }

    [HttpPost]
    public async Task<ActionResult<Discrepency>> CreateDiscrepency(Discrepency discrepancy)
    {
        return await Mediator.Send(new CreateDiscrepency.Command { Discrepency = discrepancy });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDiscrepency(string id)
    {
        await Mediator.Send(new DeleteDiscrepency.Command { Id = id });
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDiscrepency(string id, Discrepency discrepancy)
    {
        await Mediator.Send(new EditDiscrepency.Command { Id = id, Discrepency = discrepancy });
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Discrepency>> GetDiscrepencyDetails(string id)
    {
        return await Mediator.Send(new GetDiscrepencyDetails.Query { Id = id });
    }
}                   
