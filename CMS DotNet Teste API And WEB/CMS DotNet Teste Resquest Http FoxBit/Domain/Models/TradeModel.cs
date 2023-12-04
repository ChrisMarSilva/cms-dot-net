namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class TradeModel
{
    public TradeModel() 
    { 
    
    }

    public TradeModel(long id, string sn, long client_order_id, string market_symbol,
        string side, string type, string state, double price, double price_avg, 
        double quantity, double quantity_executed, double instant_amount, 
        double instant_amount_executed, DateTime created_at,int trades_count, 
        string remark, double funds_received) : this()
    {
        this.id = id;
        this.sn = sn;
        this.client_order_id = client_order_id;
        this.market_symbol = market_symbol;
        this.side = side;
        this.type = type;
        this.state = state;
        this.price = price;
        this.price_avg = price_avg;
        this.quantity = quantity;
        this.quantity_executed = quantity_executed;
        this.instant_amount = instant_amount;
        this.instant_amount_executed = instant_amount_executed;
        this.created_at = created_at;
        this.trades_count = trades_count;
        this.remark = remark;
        this.funds_received = funds_received;
    }

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

    //public override string ToString()
    //{
    //    return "";
    //}
}