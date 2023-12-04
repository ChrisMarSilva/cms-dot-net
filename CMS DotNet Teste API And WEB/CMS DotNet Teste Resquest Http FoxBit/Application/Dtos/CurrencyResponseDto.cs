namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Application.Dtos;

public class CurrencyResponseDto
{
    public string name { get; set; } = string.Empty;
    public int? precision { get; set; }
    public CurrencyCategoryResponseDto category { get; set; } = new CurrencyCategoryResponseDto();
    public string symbol { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public CurrencyDepositInfoResponseDto deposit_info { get; set; } = new CurrencyDepositInfoResponseDto();
    public CurrencyWithdrawInfoResponseDto withdraw_info { get; set; } = new CurrencyWithdrawInfoResponseDto();
}

public class CurrencyCategoryResponseDto
{
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
}


public class CurrencyDepositInfoResponseDto
{
    public string min_to_confirm { get; set; } = string.Empty;
    public string min_amount { get; set; } = string.Empty;
}

public class CurrencyWithdrawInfoResponseDto
{
    public bool? enabled { get; set; }
    public string min_amount { get; set; } = string.Empty;
    public string fee { get; set; } = string.Empty;
}
