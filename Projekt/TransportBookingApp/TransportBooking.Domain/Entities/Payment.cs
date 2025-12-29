namespace TransportBooking.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }          

    public int ReservationId { get; set; }       
    public Reservation? Reservation { get; set; }

    public decimal Amount { get; set; }
    public string Method { get; set; } = "";     
    public string PayStatus { get; set; } = ""; 
    public DateTime? PaidAt { get; set; }
}
