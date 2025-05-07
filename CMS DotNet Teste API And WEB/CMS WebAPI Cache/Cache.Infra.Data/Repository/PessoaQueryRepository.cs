using Cache.Domain.Models;
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cache.Infra.Data.Repository;

public class PessoaQueryRepository : BaseRepository<PessoaModel>, IPessoaQueryRepository
{
    public PessoaQueryRepository(AppDbContext dataContext, IUnitOfWork unitOfWork) : base(dataContext, unitOfWork)
    {
    }

    public async Task<PessoaModel?> ObterPorId(Guid id, bool incluiSituacao = false, bool disableTracking = true)
    {
        var query = DataContext.Set<PessoaModel>().AsQueryable();

        if (incluiSituacao)
            query = query.Include(x => x.Situacoes);

        if (disableTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<ICollection<PessoaModel>> ObterTodos(bool incluiSituacao = false, bool disableTracking = true)
    {
        var query = DataContext.Set<PessoaModel>().AsQueryable();

        if (incluiSituacao)
            query = query.Include(x => x.Situacoes);

        if (disableTracking)
            query = query.AsNoTracking();

        query = query.OrderBy(x => x.DtHrRegistro);

        return await query.ToListAsync();
    }
}
