namespace TransportBooking.Domain.Entities;

public class Route
{
    public long RouteId { get; set; }
    public long VehicleId { get; set; }
    public string StartCity { get; set; } = "";
    public string EndCity { get; set; } = "";
    public DateTime DepartureTime { get; set; }
    public decimal PricePerson { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

}
