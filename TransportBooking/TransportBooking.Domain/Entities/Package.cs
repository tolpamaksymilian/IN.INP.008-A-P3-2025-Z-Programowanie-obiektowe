namespace TransportBooking.Domain.Entities;

public class Package
{
    public long PackageId { get; set; }
    public long ReservationId { get; set; }
    public decimal WeightKg { get; set; }
    public string Description { get; set; } = "";

    public Reservation Reservation { get; set; }

}
