using Cache.Domain.Models;

namespace Cache.Domain.Repository;

public interface IPessoaQueryRepository
{
    Task<PessoaModel?> ObterPorId(Guid id, bool incluiSituacao = false, bool disableTracking = true);
    Task<ICollection<PessoaModel>> ObterTodos(bool incluiSituacao = false, bool disableTracking = true);
}