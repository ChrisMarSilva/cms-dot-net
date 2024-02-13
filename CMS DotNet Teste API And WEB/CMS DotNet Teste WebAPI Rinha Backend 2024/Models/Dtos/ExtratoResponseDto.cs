namespace Rinha.Backend._2024.API.Models.Dtos;

public record ExtratoResponseDto(ExtratoSaldoResponseDto saldo, ICollection<ExtratoTransacoesResponseDto>? Transacoes);
public record ExtratoSaldoResponseDto(long total, string data_extrato, long limite); // DateTime
public record ExtratoTransacoesResponseDto(long valor, string tipo, string descricao, string realizada_em);
