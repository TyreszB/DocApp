pub

namespace API.Controllers;

public class AircraftsController: BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<Aircraft>>> GetAircrafts()
    {
        return await Mediator.Send(new GetAircraftList.Query());
    }
}