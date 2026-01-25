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
                "Host=localhost;Port=5432;Database=transport_db;Username=postgres;Password=postgres");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(e =>
        {
            e.ToTable("clients");
            e.HasKey(x => x.ClientId);

            e.Property(x => x.ClientId).HasColumnName("client_id");
            e.Property(x => x.FirstName).HasColumnName("first_name");
            e.Property(x => x.LastName).HasColumnName("last_name");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Phone).HasColumnName("phone");
            e.Property(x => x.Address).HasColumnName("address");
            e.Property(x => x.PostalCode).HasColumnName("postal_code");
            e.Property(x => x.City).HasColumnName("city");
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.ToTable("vehicles");
            e.HasKey(x => x.VehicleId);

            e.Property(x => x.VehicleId).HasColumnName("vehicle_id");
            e.Property(x => x.PlateNumber).HasColumnName("plate_number");
            e.Property(x => x.Model).HasColumnName("model");
            e.Property(x => x.Seats).HasColumnName("seats");
            e.Property(x => x.Active).HasColumnName("active");
        });

        modelBuilder.Entity<Route>(e =>
        {
            e.ToTable("routes");
            e.HasKey(x => x.RouteId);

            e.Property(x => x.RouteId).HasColumnName("route_id");
            e.Property(x => x.VehicleId).HasColumnName("vehicle_id");
            e.Property(x => x.StartCity).HasColumnName("start_city");
            e.Property(x => x.EndCity).HasColumnName("end_city");
            e.Property(x => x.DepartureTime).HasColumnName("departure_time");
            e.Property(x => x.PricePerson).HasColumnName("price_person");
        });

        modelBuilder.Entity<Reservation>(e =>
        {
            e.ToTable("reservations");
            e.HasKey(x => x.ReservationId);

            e.Property(x => x.ReservationId).HasColumnName("reservation_id");
            e.Property(x => x.ClientId).HasColumnName("client_id");
            e.Property(x => x.RouteId).HasColumnName("route_id");
            e.Property(x => x.ServiceType).HasColumnName("service_type");
            e.Property(x => x.Status).HasColumnName("status");
            e.Property(x => x.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Payment>(e =>
        {
            e.ToTable("payments");
            e.HasKey(x => x.PaymentId);

            e.Property(x => x.PaymentId).HasColumnName("payment_id");
            e.Property(x => x.ReservationId).HasColumnName("reservation_id");
            e.Property(x => x.Amount).HasColumnName("amount");
            e.Property(x => x.PaidAt).HasColumnName("paid_at");
            e.Property(x => x.Method).HasColumnName("method");
            e.Property(x => x.PayStatus).HasColumnName("pay_status");
        });

        modelBuilder.Entity<Package>(e =>
        {
            e.ToTable("packages");
            e.HasKey(x => x.PackageId);

            e.Property(x => x.PackageId).HasColumnName("package_id");
            e.Property(x => x.ReservationId).HasColumnName("reservation_id");
            e.Property(x => x.WeightKg).HasColumnName("weight_kg");
            e.Property(x => x.Description).HasColumnName("description");
        });
    }

}
