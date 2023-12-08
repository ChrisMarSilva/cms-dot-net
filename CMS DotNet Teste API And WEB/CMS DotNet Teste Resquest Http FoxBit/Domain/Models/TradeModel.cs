namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;
public class TradeModel
{
    public TradeModel() 
    { 
    
    }

    public TradeModel(long id, string sn, long order_id, string market_symbol,
        string side, double price, double quantity, double fee, 
        string fee_currency_symbol, DateTime created_at) : this()
    {
        this.id = id;
        this.sn = sn;
        this.order_id = order_id;
        this.market_symbol = market_symbol;
        this.side = side;
        this.price = price;
        this.quantity = quantity;
        this.fee = fee;
        this.fee_currency_symbol = fee_currency_symbol;
        this.created_at = created_at;
    }
   
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

    //public override string ToString()
    //{
    //    return "";
    //}
}