using System;

namespace Domain;

public class Aircraft
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string SerialNumber { get; set; }

    public required string Model { get; set; }

    public required string AircraftStatus { get; set; }

    public required string AircraftLocation { get; set; }
    
       

    public Discrepency[]? Discrepencies { get; set; } = [];




}
