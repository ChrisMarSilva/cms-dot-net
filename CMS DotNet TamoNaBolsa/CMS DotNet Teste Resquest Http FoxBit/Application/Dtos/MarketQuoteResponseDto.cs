namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class MarketQuotesResponseDto
{
    public string side { get; set; }
    public string market_symbol { get; set; }
    public string base_amount { get; set; }
    public string quote_amount { get; set; }
    public string price { get; set; }
}

