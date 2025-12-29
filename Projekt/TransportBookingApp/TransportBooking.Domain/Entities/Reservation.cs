namespace TransportBooking.Domain.Entities;

public class Reservation
{
    public int ReservationId { get; set; }         

    public int ClientId { get; set; }               
    public Client? Client { get; set; }

    public int RouteId { get; set; }               
    public Route? Route { get; set; }

    public string ServiceType { get; set; } = "PERSON";

    public string Status { get; set; } = "NEW";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Package? Package { get; set; }

    public List<Payment> Payments { get; set; } = new();
}
