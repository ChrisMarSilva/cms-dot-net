using Cache.Domain.Models;

namespace Cache.Domain.Repository;

public interface IPessoaCommandRepository : IBaseRepository<PessoaModel>
{
    void Add(PessoaModel entity);
}
