namespace TransportBooking.Domain.Entities;

public class Reservation
{
    public long ReservationId { get; set; }
    public long ClientId { get; set; }
    public long RouteId { get; set; }
    public string ServiceType { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }

    public Client Client { get; set; }

    public Route Route { get; set; }

}
