namespace TransportBooking.Domain.Entities;

public class Payment
{
    public long PaymentId { get; set; }
    public long ReservationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? PaidAt { get; set; }
    public string Method { get; set; } = "";
    public string PayStatus { get; set; } = "";
}
