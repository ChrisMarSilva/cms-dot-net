using Cache.Domain.Models;

namespace Cache.Domain.Repository;

public interface ItQueryRepository
{
    Task<PessoaModel?> ObterPessoaPorIda(string id, bool incluiSituacao = false, bool disableTracking = true);
    Task<ICollection<PessoaModel>> ObterPessoas(DateTime limiteInicio, DateTime limiteFinal);
}