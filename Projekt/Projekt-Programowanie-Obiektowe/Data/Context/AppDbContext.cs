using Microsoft.EntityFrameworkCore;
using System.IO.Packaging;
using Projekt-Programowanie - Obiektowe.Domain.Entities;

namespace Projekt-Programowanie-Obiektowe.Data.Context;

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
        modelBuilder.Entity<Client>().HasKey(x => x.ClientId);
        modelBuilder.Entity<Vehicle>().HasKey(x => x.VehicleId);
        modelBuilder.Entity<Route>().HasKey(x => x.RouteId);
        modelBuilder.Entity<Reservation>().HasKey(x => x.ReservationId);
        modelBuilder.Entity<Package>().HasKey(x => x.PackageId);
        modelBuilder.Entity<Payment>().HasKey(x => x.PaymentId);

        modelBuilder.Entity<Client>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Vehicle>().HasIndex(x => x.PlateNumber).IsUnique();

        modelBuilder.Entity<Route>()
            .HasOne(r => r.Vehicle)
            .WithMany(v => v.Routes)
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Route)
            .WithMany(ro => ro.Reservations)
            .HasForeignKey(r => r.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Package)
            .WithOne(p => p.Reservation)
            .HasForeignKey<Package>(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Reservation)
            .WithMany(r => r.Payments)
            .HasForeignKey(p => p.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
