namespace TransportBooking.Domain.Entities;

public class Vehicle
{
    public int VehicleId { get; set; }         
    public string PlateNumber { get; set; } = ""; 
    public string Model { get; set; } = "";
    public int Seats { get; set; }
    public bool Active { get; set; } = true;

    public List<Route> Routes { get; set; } = new();
}
