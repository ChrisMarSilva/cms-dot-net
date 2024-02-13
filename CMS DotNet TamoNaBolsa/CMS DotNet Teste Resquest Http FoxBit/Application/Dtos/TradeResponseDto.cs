using System.Text.Json.Serialization;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class TradetRootResponseDto
{
    public List<TradeResponseDto> data { get; set; } = new List<TradeResponseDto>();
}

public class TradeResponseDto
{
    public int id { get; set; }
    public string sn { get; set; } = string.Empty;
    public string order_id { get; set; } = string.Empty;
    public string market_symbol { get; set; } = string.Empty;
    public string side { get; set; } = string.Empty;
    public string price { get; set; } = string.Empty;
    public string quantity { get; set; } = string.Empty;
    public string fee { get; set; }
    public string fee_currency_symbol { get; set; } = string.Empty;
    public DateTime created_at { get; set; }
}
