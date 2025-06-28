using System;
using Microsoft.VisualBasic;
using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if(context.Users.Any()) return;

        var users = new List<User>
        {   
            new() {
                Name = "Bob",
                Email = "bob@test.com",
                Password = "password",
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsEmailVerified = true,
                IsPhoneVerified = true,
                IsActive = true,
                IsArchived = false,
                IsLocked = false,
                IsVerified = true,
            }
        };

         context.Users.AddRange(users);
         await context.SaveChangesAsync();
    }
}