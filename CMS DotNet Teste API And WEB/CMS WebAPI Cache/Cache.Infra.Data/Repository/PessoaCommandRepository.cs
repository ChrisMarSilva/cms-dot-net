using Cache.Domain.Models;
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;

namespace Cache.Infra.Data.Repository;

public class PessoaCommandRepository : BaseRepository<PessoaModel>, IPessoaCommandRepository
{
    public PessoaCommandRepository(AppDbContext dataContext, IUnitOfWork unitOfWork) : base(dataContext, unitOfWork)
    {
    }

    public void Add(PessoaModel entity)
    {
        DataContext.Set<PessoaModel>().Add(entity);
    }
}