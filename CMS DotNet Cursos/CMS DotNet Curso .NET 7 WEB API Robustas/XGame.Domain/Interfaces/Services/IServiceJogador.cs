using XGame.Domain.Arguments.Base;
using XGame.Domain.Arguments.Jogador;

namespace XGame.Domain.Interfaces.Services;

public interface IServiceJogador : IServiceBase
{
    AutenticarJogadorResponse AutenticarJogador(AutenticarJogadorRequest request);
    AdicionarJogadorResponse AdicionarJogador(AdicionarJogadorRequest request);
    AlterarJogadorResponse AlterarJogador(AlterarJogadorRequest request);
    IEnumerable<JogadorResponse> ListarJogador();
    ResponseBase ExcluirJogador(Guid id);
}
