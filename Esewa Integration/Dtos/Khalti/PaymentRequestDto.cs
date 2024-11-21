namespace Esewa_Integration.Dtos.Khalti
{
    public class PaymentRequestDto
    {
        public string return_url { get; set; }
        public string website_url { get; set; }
        public int amount { get; set; }
        public string purchase_order_id
        {
            get; set;
        }
        public string purchase_order_name { get; set; }
        public CustomerInfo customer_info { get; set; }
        public AmountBreakdown[] amount_breakdown { get; set; }
        public ProductDetails[] product_details { get; set; }
        public string? merchant_username { get; set; }
        public string? merchant_extra { get; set; }
    }

    public class CustomerInfo
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class AmountBreakdown
    {
        public string label { get; set; }
        public int amount { get; set; }
    }

    public class ProductDetails
    {
        public string identity { get; set; }
        public string name { get; set; }
        public int total_price { get; set; }
        public int quantity { get; set; }
        public int unit_price { get; set; }
    }
}
