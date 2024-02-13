namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;

public class MarketModel
{
    public string symbol { get; set; } = string.Empty;
    public double quantity_min { get; set; }
    public double quantity_increment { get; set; }
    public double price_min { get; set; }
    public double price_increment { get; set; }
    public string base_name { get; set; } = string.Empty;
    public int base_precision { get; set; }
    public string base_symbol { get; set; } = string.Empty;
    public string base_type { get; set; } = string.Empty;
    public string quote_name { get; set; } = string.Empty;
    public int quote_precision { get; set; }
    public string quote_symbol { get; set; } = string.Empty;
    public string quote_type { get; set; } = string.Empty;
}