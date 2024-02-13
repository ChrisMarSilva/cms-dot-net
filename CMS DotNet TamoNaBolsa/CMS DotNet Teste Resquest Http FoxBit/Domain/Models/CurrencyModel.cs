namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class CurrencyModel
{
    public CurrencyModel()
    {

    }

    public CurrencyModel(string symbol, string name, int? precision, string category_code, 
        string category_name, string type, double deposit_min_to_confirm, 
        double deposit_min_amount, bool? withdraw_enabled, double withdraw_min_amount, 
        double withdraw_fee) : this()
    {
        this.symbol = symbol;
        this.name = name;
        this.precision = precision;
        this.category_code = category_code;
        this.category_name = category_name;
        this.type = type;
        this.deposit_min_to_confirm = deposit_min_to_confirm;
        this.deposit_min_amount = deposit_min_amount;
        this.withdraw_enabled = withdraw_enabled;
        this.withdraw_min_amount = withdraw_min_amount;
        this.withdraw_fee = withdraw_fee;
    }

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

    //public override string ToString()
    //{
    //    return "";
    //}
}
