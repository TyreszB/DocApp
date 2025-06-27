using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private static List<Document> _documents = new();

    [HttpGet]
    public ActionResult<IEnumerable<Document>> GetAll()
    {
        return Ok(_documents);
    }

    [HttpGet("{id}")]
    public ActionResult<Document> GetById(Guid id)
    {
        var document = _documents.FirstOrDefault(d => d.Id == id);
        if (document == null)
            return NotFound();
        
        return Ok(document);
    }

    [HttpPost]
    public ActionResult<Document> Create(Document document)
    {
        document.Id = Guid.NewGuid();
        _documents.Add(document);
        return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
    }

    [HttpPut("{id}")]
    public ActionResult<Document> Update(Guid id, Document document)
    {
        var existingDocument = _documents.FirstOrDefault(d => d.Id == id);
        if (existingDocument == null)
            return NotFound();

        existingDocument.Name = document.Name;
        existingDocument.Type = document.Type;
        existingDocument.Path = document.Path;
        existingDocument.Description = document.Description;
        existingDocument.Aircraft = document.Aircraft;
        existingDocument.Discrepency = document.Discrepency;

        return Ok(existingDocument);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var document = _documents.FirstOrDefault(d => d.Id == id);
        if (document == null)
            return NotFound();

        _documents.Remove(document);
        return NoContent();
    }

    [HttpGet("by-aircraft/{aircraftId}")]
    public ActionResult<IEnumerable<Document>> GetByAircraft(Guid aircraftId)
    {
        var documents = _documents.Where(d => d.Aircraft.Id == aircraftId);
        return Ok(documents);
    }

    [HttpGet("by-type/{type}")]
    public ActionResult<IEnumerable<Document>> GetByType(string type)
    {
        var documents = _documents.Where(d => d.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        return Ok(documents);
    }
} 