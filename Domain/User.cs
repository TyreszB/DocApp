using System;

namespace Domain;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsEmailVerified { get; set; } = false;
    public bool IsPhoneVerified { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsArchived { get; set; } = false;
    public bool IsLocked { get; set; } = false;
    public bool IsVerified { get; set; } = false;
   
}

public enum UserRole
{
    Admin,
    User,
    Technician,
    QualityControl,
    AircraftSupervisor,
    Pilot,  
    TechnicalWriter,

}
