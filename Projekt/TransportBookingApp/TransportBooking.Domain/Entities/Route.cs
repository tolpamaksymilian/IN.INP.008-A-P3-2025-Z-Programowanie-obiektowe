namespace TransportBooking.Domain.Entities;

public class Route
{
    public int RouteId { get; set; }            

    public int VehicleId { get; set; }          
    public Vehicle? Vehicle { get; set; }

    public string StartCity { get; set; } = "";
    public string EndCity { get; set; } = "";
    public DateTime DepartureTime { get; set; }
    public decimal PricePerson { get; set; }

    public List<Reservation> Reservations { get; set; } = new();
}
