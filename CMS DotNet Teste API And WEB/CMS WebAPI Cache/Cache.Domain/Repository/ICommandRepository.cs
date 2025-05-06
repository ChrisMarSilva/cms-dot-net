using Cache.Domain.Models;

namespace Cache.Domain.Repository;

public interface ICommandRepository : IRepository
{
    void Add(PessoaModel pessoaModel);
}
