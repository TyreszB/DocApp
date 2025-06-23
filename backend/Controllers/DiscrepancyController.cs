using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscrepancyController : ControllerBase
{
    private static List<Discrepency> _discrepancies = new();

    [HttpGet]
    public ActionResult<IEnumerable<Discrepency>> GetAll()
    {
        return Ok(_discrepancies);
    }

    [HttpGet("{id}")]
    public ActionResult<Discrepency> GetById(Guid id)
    {
        var discrepancy = _discrepancies.FirstOrDefault(d => d.Id == id);
        if (discrepancy == null)
            return NotFound();
        
        return Ok(discrepancy);
    }

    [HttpPost]
    public ActionResult<Discrepency> Create(Discrepency discrepancy)
    {
        discrepancy.Id = Guid.NewGuid();
        _discrepancies.Add(discrepancy);
        return CreatedAtAction(nameof(GetById), new { id = discrepancy.Id }, discrepancy);
    }

    [HttpPut("{id}")]
    public ActionResult<Discrepency> Update(Guid id, Discrepency discrepancy)
    {
        var existingDiscrepancy = _discrepancies.FirstOrDefault(d => d.Id == id);
        if (existingDiscrepancy == null)
            return NotFound();

        existingDiscrepancy.Description = discrepancy.Description;
        existingDiscrepancy.Status = discrepancy.Status;
        existingDiscrepancy.Priority = discrepancy.Priority;
        existingDiscrepancy.Type = discrepancy.Type;
        existingDiscrepancy.Location = discrepancy.Location;
        existingDiscrepancy.Technicians = discrepancy.Technicians;
        existingDiscrepancy.Aircraft = discrepancy.Aircraft;

        return Ok(existingDiscrepancy);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var discrepancy = _discrepancies.FirstOrDefault(d => d.Id == id);
        if (discrepancy == null)
            return NotFound();

        _discrepancies.Remove(discrepancy);
        return NoContent();
    }

    [HttpGet("by-status/{status}")]
    public ActionResult<IEnumerable<Discrepency>> GetByStatus(DiscrepencyStatus status)
    {
        var discrepancies = _discrepancies.Where(d => d.Status == status);
        return Ok(discrepancies);
    }
} 