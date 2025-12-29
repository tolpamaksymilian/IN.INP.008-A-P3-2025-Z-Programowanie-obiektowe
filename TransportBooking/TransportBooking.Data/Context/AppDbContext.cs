using Microsoft.EntityFrameworkCore;
using TransportBooking.Domain.Entities;

namespace TransportBooking.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Package> Packages => Set<Package>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=transportbooking;Username=postgres;Password=postgres");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("clients").HasKey(c => c.ClientId);
        modelBuilder.Entity<Vehicle>().ToTable("vehicles").HasKey(v => v.VehicleId);
        modelBuilder.Entity<Route>().ToTable("routes").HasKey(r => r.RouteId);
        modelBuilder.Entity<Reservation>().ToTable("reservations").HasKey(r => r.ReservationId);
        modelBuilder.Entity<Payment>().ToTable("payments").HasKey(p => p.PaymentId);
        modelBuilder.Entity<Package>().ToTable("packages").HasKey(p => p.PackageId);
    }
}
