﻿namespace Esewa_Integration.Dtos.Khalti;

public class PaymentVerificationResponseDto
{
    public string pidx { get; set; }
    public int total_amount { get; set; }
    public string status { get; set; }
    public string transaction_id { get; set; }
    public int fee { get; set; }
    public bool refunded { get; set; }

}
