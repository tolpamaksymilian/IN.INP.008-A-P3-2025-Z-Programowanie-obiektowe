namespace TransportBooking.Domain.Entities;

public class Vehicle
{
    public long VehicleId { get; set; }
    public string PlateNumber { get; set; } = "";
    public string Model { get; set; } = "";
    public int Seats { get; set; }
    public bool Active { get; set; }
}
