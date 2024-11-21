namespace Esewa_Integration.Dtos.Khalti;
public class PaymentRequestSuccessResponseDto
{
    public string pidx { get; set; }
    public string payment_url { get; set; }
    public DateTime expires_at { get; set; }
    public int expires_in { get; set; }
}
