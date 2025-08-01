using System;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if(context.Users.Any()) return;

        var passwordHasher = new PasswordHasher<User>();
        var user = new User
        {
            Name = "Bob",
            Email = "bob@test.com",
            Password = "password",
            Role = "Admin",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            RefreshToken = string.Empty,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(1),
            IsActive = true,
            IsArchived = false,
            IsLocked = false,
            IsVerified = true,
        };

        // Hash the password
        user.PasswordHash = passwordHasher.HashPassword(user, user.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Seed Aircrafts
        if (context.Aircrafts.Any()) return;

        var aircrafts = new List<Aircraft>
        {
            new Aircraft
            {
                SerialNumber = "1234567891",
                Model = "Model 1",
                AircraftStatus = "Status 1",
                AircraftLocation = "Location 1",
                Discrepencies = [],
            },
            new Aircraft
            {
                SerialNumber = "1234567890",
                Model = "Model 2",
                AircraftStatus = "Status 2",
                AircraftLocation = "Location 2",
                Discrepencies = []
            }
        };
        context.Aircrafts.AddRange(aircrafts);
        await context.SaveChangesAsync();
        

        // Seed Discrepencies
        if (context.Discrepencies.Any()) return;
        
        var discrepancies = new List<Discrepency>
        {
            new Discrepency
            {
                Aircraft = null, // You can assign an aircraft if needed
                DiscrepencyType = "Type 1",
                DiscrepencyDescription = "Description 1",
                DiscrepencyStatus = "Status 1",
                DiscrepencyPriority = "Priority 1",
                DocumentUrl = "DocumentUrl 1"                
            },
            new Discrepency
            {
                Aircraft = null, // You can assign an aircraft if needed
                DiscrepencyType = "Type 2",
                DiscrepencyDescription = "Description 2",
                DiscrepencyStatus = "Status 2",
                DiscrepencyPriority = "Priority 2",
                DocumentUrl = "DocumentUrl 2"
            }
        };
        context.Discrepencies.AddRange(discrepancies);
        await context.SaveChangesAsync();
        
    }
} 