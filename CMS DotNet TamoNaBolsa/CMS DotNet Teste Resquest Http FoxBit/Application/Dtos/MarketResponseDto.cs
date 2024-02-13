namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class MarketRootResponseDto
{
    public List<MarketResponseDto> data { get; set; }
}

public class MarketResponseDto
{
    public MarketBaseResponseDto @base { get; set; }
    public MarketQuoteResponseDto quote { get; set; }
    public string symbol { get; set; }
    public string quantity_min { get; set; }
    public string quantity_increment { get; set; }
    public string price_min { get; set; }
    public string price_increment { get; set; }
}

public class MarketBaseResponseDto
{
    public string name { get; set; }
    public int precision { get; set; }
    public string symbol { get; set; }
    public string type { get; set; }
}

public class MarketQuoteResponseDto
{
    public string name { get; set; }
    public int precision { get; set; }
    public string symbol { get; set; }
    public string type { get; set; }
}