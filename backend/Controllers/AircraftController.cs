using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AircraftController : ControllerBase
{
    private static List<Aircraft> _aircraft = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Aircraft>> GetAll()
    {
        return Ok(_aircraft);
    }

    [HttpGet("{id}")]
    public ActionResult<Aircraft> GetById(Guid id)
    {
        var aircraft = _aircraft.FirstOrDefault(a => a.Id == id);
        if (aircraft == null)
            return NotFound();
        
        return Ok(aircraft);
    }

    [HttpPost]
    public ActionResult<Aircraft> Create(Aircraft aircraft)
    {
        aircraft.Id = Guid.NewGuid();
        _aircraft.Add(aircraft);
        return CreatedAtAction(nameof(GetById), new { id = aircraft.Id }, aircraft);
    }

    [HttpPut("{id}")]
    public ActionResult<Aircraft> Update(Guid id, Aircraft aircraft)
    {
        var existingAircraft = _aircraft.FirstOrDefault(a => a.Id == id);
        if (existingAircraft == null)
            return NotFound();

        existingAircraft.SerialNumber = aircraft.SerialNumber;
        existingAircraft.Type = aircraft.Type;
        existingAircraft.Model = aircraft.Model;
        existingAircraft.Location = aircraft.Location;
        existingAircraft.Status = aircraft.Status;
        existingAircraft.Discrepencys = aircraft.Discrepencys;

        return Ok(existingAircraft);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var aircraft = _aircraft.FirstOrDefault(a => a.Id == id);
        if (aircraft == null)
            return NotFound();

        _aircraft.Remove(aircraft);
        return NoContent();
    }
} 