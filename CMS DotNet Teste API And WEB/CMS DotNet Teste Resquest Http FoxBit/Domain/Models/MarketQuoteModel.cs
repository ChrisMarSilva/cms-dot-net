namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class MarketQuoteModel
{
    public MarketQuoteModel()
    {

    }

    public MarketQuoteModel(string market_symbol, string side, double base_amount, int quote_amount, double price) : this()
    {
        this.market_symbol = market_symbol;
        this.side = side;
        this.base_amount = base_amount;
        this.quote_amount = quote_amount;
        this.price = price;
    }

    public string market_symbol { get; set; } = string.Empty;
    public string side { get; set; } = string.Empty;
    public double base_amount { get; set; }
    public int quote_amount { get; set; }
    public double price { get; set; }

    //public override string ToString()
    //{
    //    return "";
    //}
}
