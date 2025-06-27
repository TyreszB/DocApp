public class Aircraft
{
    public Guid Id { get; set; }
    public required int SerialNumber { get; set; }
    public required string Type { get; set; }

    public required string Model { get; set; }
   
    public required string Location { get; set; }
    public required AircraftStatus Status { get; set; }

    public required Discrepency[] Discrepencys { get; set; }
}

public enum AircraftStatus{
    InService,
    OutOfService,
    InMaintenance,
    InStorage,
    InTransit,
    
    InInspection,
   
}