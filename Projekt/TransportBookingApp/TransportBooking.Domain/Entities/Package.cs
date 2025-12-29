namespace TransportBooking.Domain.Entities;

public class Package
{
    public int PackageId { get; set; }          

    public int ReservationId { get; set; }     
    public Reservation? Reservation { get; set; }

    public decimal WeightKg { get; set; }
    public string Description { get; set; } = "";
}
