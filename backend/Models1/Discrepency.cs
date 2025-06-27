using System.Runtime.CompilerServices;

public class Discrepency
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public required DiscrepencyStatus Status { get; set; }
    public required string Priority { get; set; }
    public required string Type { get; set; }
    public required string Location { get; set; } 
    public required User[] Technicians { get; set; }
    public required Aircraft Aircraft { get; set; }
  
}

public enum DiscrepencyStatus 
{
    Open,
    InProgress,
    Closed,
    NeedsReview,
    Archived,
    
    Approved,
    
   
}