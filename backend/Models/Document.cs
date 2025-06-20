public class Document
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Path { get; set; }
    public required string Description { get; set; }
    public required Aircraft Aircraft { get; set; }
    public required Discrepency Discrepency { get; set; }
}