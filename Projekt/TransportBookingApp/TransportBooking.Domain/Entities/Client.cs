using TransportBooking.Domain.Entities;

public class Client
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";

    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<Reservation> Reservations { get; set; } = new();
}
