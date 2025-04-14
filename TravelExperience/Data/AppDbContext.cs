using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Trip> Trips { get; set; }         // Trips table
    public DbSet<Activity> Activities { get; set; } // Activities table
}
