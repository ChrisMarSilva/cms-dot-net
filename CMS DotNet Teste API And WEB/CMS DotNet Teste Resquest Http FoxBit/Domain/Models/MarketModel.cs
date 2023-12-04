namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class MarketModel
{
    public MarketModel()
    {

    }

    public MarketModel(string symbol, double quantity_min, double quantity_increment, 
        double price_min, double price_increment, string base_name, int base_precision, 
        string base_symbol, string base_type, string quote_name, int quote_precision, 
        string quote_symbol, string quote_type) : this()
    {
        this.symbol = symbol;
        this.quantity_min = quantity_min;
        this.quantity_increment = quantity_increment;
        this.price_min = price_min;
        this.price_increment = price_increment;
        this.base_name = base_name;
        this.base_precision = base_precision;
        this.base_symbol = base_symbol;
        this.base_type = base_type;
        this.quote_name = quote_name;
        this.quote_precision = quote_precision;
        this.quote_symbol = quote_symbol;
        this.quote_type = quote_type;
    }

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

    //public override string ToString()
    //{
    //    return "";
    //}
}