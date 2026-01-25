namespace TransportBooking.Domain.Entities;

public class Payment
{
    public long PaymentId { get; set; }
    public long ReservationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? PaidAt { get; set; }
    public string Method { get; set; } = "";
    public string PayStatus { get; set; } = "";

    public ICollection<Payment> Payments { get; set; }
    public ICollection<Package> Packages { get; set; }
}
