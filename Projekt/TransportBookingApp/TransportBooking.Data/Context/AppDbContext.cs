using Microsoft.EntityFrameworkCore;
using TransportBooking.Domain.Entities;

namespace TransportBooking.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(e =>
        {
            e.HasKey(x => x.ClientId);
            e.HasIndex(x => x.Email).IsUnique();

            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(150).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.Address).HasMaxLength(200);
            e.Property(x => x.PostalCode).HasMaxLength(20);
            e.Property(x => x.City).HasMaxLength(80);
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.HasKey(x => x.VehicleId);
            e.HasIndex(x => x.PlateNumber).IsUnique();

            e.Property(x => x.PlateNumber).HasMaxLength(20).IsRequired();
            e.Property(x => x.Model).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Route>(e =>
        {
            e.HasKey(x => x.RouteId);

            e.Property(x => x.StartCity).HasMaxLength(80).IsRequired();
            e.Property(x => x.EndCity).HasMaxLength(80).IsRequired();

            e.HasOne(x => x.Vehicle)
                .WithMany(v => v.Routes)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Reservation>(e =>
        {
            e.HasKey(x => x.ReservationId);

            e.Property(x => x.ServiceType).HasMaxLength(30).IsRequired();
            e.Property(x => x.Status).HasMaxLength(30).IsRequired();

            e.HasOne(x => x.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Route)
                .WithMany(r => r.Reservations)
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Package)
                .WithOne(p => p.Reservation)
                .HasForeignKey<Package>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Payments)
                .WithOne(p => p.Reservation)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Package>(e =>
        {
            e.HasKey(x => x.PackageId);
            e.HasIndex(x => x.ReservationId).IsUnique();

            e.Property(x => x.Description).HasMaxLength(250);
        });

        // Payments
        modelBuilder.Entity<Payment>(e =>
        {
            e.HasKey(x => x.PaymentId);

            e.Property(x => x.Method).HasMaxLength(30).IsRequired();
            e.Property(x => x.PayStatus).HasMaxLength(30).IsRequired();
        });
    }
}
