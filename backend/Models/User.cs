public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }  

    public required UserRole Position { get; set; }

    public required bool IsActive { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}


public enum UserRole
{
    Admin,
    Technician,
    QualityControl,
    TechSupervisor,
    AircraftSupervisor,
    Manager,


    
}