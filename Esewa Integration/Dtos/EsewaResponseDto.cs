﻿namespace Esewa_Integration.Dtos;

public class EsewaResponseDto
{
    public string transaction_code { get; set; }
    public string status { get; set; }
    public string total_amount { get; set; }
    public string transaction_uuid { get; set; }
    public string product_code { get; set; }
    public string signed_field_names { get; set; }
    public string signature { get; set; }

}
