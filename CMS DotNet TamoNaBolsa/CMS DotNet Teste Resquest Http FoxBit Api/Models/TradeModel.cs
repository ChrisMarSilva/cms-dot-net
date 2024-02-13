namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;

public class TradeModel
{
    public long id { get; set; }
    public string sn { get; set; } = string.Empty;
    public long order_id { get; set; }
    public string market_symbol { get; set; } = string.Empty;
    public string side { get; set; } = string.Empty;
    public double price { get; set; }
    public double quantity { get; set; }
    public double fee { get; set; }
    public string fee_currency_symbol { get; set; } = string.Empty;
    public DateTime created_at { get; set; }
}