namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;

public class CurrencyModel
{
    public string symbol { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public int? precision { get; set; }
    public string category_code { get; set; } = string.Empty;
    public string category_name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public double deposit_min_to_confirm { get; set; }
    public double deposit_min_amount { get; set; }
    public bool? withdraw_enabled { get; set; }
    public double withdraw_min_amount { get; set; }
    public double withdraw_fee { get; set; }
}
