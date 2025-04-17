namespace CMS_Web_ByBit_Misc_Console;

public class Registro
{
    public long Uid { get; set; } //ID do Usuário				Identificador único da sua conta na Bybit.
    public string Currency { get; set; } // Moeda						Moeda utilizada na transação (ex: USDT, BTC).
    public string Contract { get; set; } // Contrato					Par de negociação ou contrato derivativo (ex: AEROUSDT).
    public string Type { get; set; } // Tipo						Tipo de operação (ex: TRADE para negociação, FUNDING para taxa de financiamento).
    public string Direction { get; set; } // Direção						Indica se a operação foi de compra (BUY) ou venda (SELL).
    public decimal Quantity { get; set; } // Quantidade					Quantidade de contratos ou ativos envolvidos na operação.
    public decimal Position { get; set; } // Posição						Tamanho da posição após a operação.
    public decimal FilledPrice { get; set; } // Preço Executado				Preço pelo qual a ordem foi executada.
    public decimal Funding { get; set; } // Taxa de Financiamento		Valor pago ou recebido como taxa de financiamento em contratos perpétuos.
    public decimal FeePaid { get; set; } // Taxa Paga					Valor pago em taxas pela execução da ordem.
    public decimal CashFlow { get; set; } // Fluxo de Caixa				Valor líquido movimentado na operação (positivo para entrada, negativo para saída).
    public decimal Change { get; set; } // Alteração					Mudança no saldo da carteira devido à operação.
    public decimal WalletBalance { get; set; } // Balance	Saldo da Carteira	Saldo total da carteira após a operação.
    public string Action { get; set; } // Ação						Ação específica realizada (pode estar em branco se não aplicável).
    public DateTime TimeUTC { get; set; } // Data e Hora (UTC)			Data e hora em que a operação foi registrada, no formato UTC.
}


//Console.WriteLine(
//    $"Uid: {r.Uid}, " +
//    $"Currency: {r.Currency}, " +
//    $"Contract: {r.Contract}, " +
//    $"Type: {r.Type}, " +
//    $"Direction: {r.Direction}, " +
//    $"Quantity: {r.Quantity}, " +
//    $"Position: {r.Position}, " +
//    $"Filled Price: {r.FilledPrice}, " +
//    $"Funding: {r.Funding}, " +
//    $"Fee Paid: {r.FeePaid}, " +
//    $"Cash Flow: {r.CashFlow}, " +
//    $"Change: {r.Change}, " +
//    $"Wallet Balance: {r.WalletBalance}, " +
//    $"Action: {r.Action}, " +
//    $"Time (UTC): {r.TimeUTC:yyyy-MM-dd HH:mm:ss}");
