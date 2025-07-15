using System;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : Microsoft.AspNetCore.Identity.IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsEmailVerified { get; set; } = false;
    public bool IsPhoneVerified { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsArchived { get; set; } = false;
    public bool IsLocked { get; set; } = false;
    public bool IsVerified { get; set; } = false;
}


