using System;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Discrepency> Discrepencies { get; set; }
}
