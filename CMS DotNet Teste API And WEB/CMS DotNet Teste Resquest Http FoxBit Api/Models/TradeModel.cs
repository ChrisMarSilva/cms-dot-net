namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models;

public class TradeModel
{
    public long id { get; set; }
    public string sn { get; set; } = string.Empty;
    public long client_order_id { get; set; }
    public string market_symbol { get; set; } = string.Empty;
    public string side { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string state { get; set; } = string.Empty;
    public double price { get; set; }
    public double price_avg { get; set; }
    public double quantity { get; set; }
    public double quantity_executed { get; set; }
    public double instant_amount { get; set; }
    public double instant_amount_executed { get; set; } 
    public DateTime created_at { get; set; }
    public int trades_count { get; set; }
    public string remark { get; set; } = string.Empty;
    public double funds_received { get; set; }
}