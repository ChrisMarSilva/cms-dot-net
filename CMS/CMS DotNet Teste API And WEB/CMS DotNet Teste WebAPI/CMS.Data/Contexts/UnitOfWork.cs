using System.Threading.Tasks;

namespace CMS.Data.Contexts
{
    public class UnitOfWork : IUnitofWork
    {
        private BancoDeDadosContext _ctx;

        public UnitOfWork(BancoDeDadosContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task CommitAsync()
        {
            await this._ctx.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            return null;
        }
    }
}
