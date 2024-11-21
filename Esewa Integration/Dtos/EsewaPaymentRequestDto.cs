namespace Esewa_Integration.Dtos;

public class EsewaPaymentRequestDto
{
    public string amount { get; set; }
    public string failure_url { get; set; } = "https://google.com";
    public string product_delivery_charge { get; set; } = "0";
    public string product_service_charge { get; set; } = "0";
    public string product_code { get; set; } = "EPAYTEST";
    public string signature { get; set; }
    public string signed_field_names { get; set; } = "total_amount,transaction_uuid,product_code";
    public string success_url { get; set; } = "https://localhost:7139/api/payment/verify";
    public string tax_amount { get; set; } = "0";
    public string total_amount { get; set; }
    public string transaction_uuid { get; set; } = Guid.NewGuid().ToString();

}
