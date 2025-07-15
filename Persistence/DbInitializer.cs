using System;
using Microsoft.AspNetCore.Identity;
using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create roles if they don't exist
        var roles = new[] { "Admin", "User", "Manager" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create admin user if no users exist
        if (!userManager.Users.Any())
        {
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@test.com",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsEmailVerified = true,
                IsPhoneVerified = true,
                IsActive = true,
                IsArchived = false,
                IsLocked = false,
                IsVerified = true,
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}