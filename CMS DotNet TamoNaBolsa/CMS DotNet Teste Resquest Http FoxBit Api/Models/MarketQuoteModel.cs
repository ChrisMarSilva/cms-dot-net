namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;

public class MarketQuoteModel
{
    public string side { get; set; } = string.Empty;
    public string market_symbol { get; set; } = string.Empty;
    public double base_amount { get; set; }
    public int quote_amount { get; set; }
    public double price { get; set; }
}
