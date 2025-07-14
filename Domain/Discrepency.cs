using System;

namespace Domain;

public class Discrepency
{

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public required Aircraft Aircraft { get; set; }

    public required string DiscrepencyType { get; set; }
    public required string DiscrepencyDescription { get; set; }

    public required string DiscrepencyStatus { get; set; }

    public required string DiscrepencyPriority { get; set; }
    public required string DocumentUrl { get; set; }

    



}
