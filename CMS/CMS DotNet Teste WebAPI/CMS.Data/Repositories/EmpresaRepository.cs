using CMS.Data.Contexts;
using CMS.Domain.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class EmpresaRepository : Repository<Empresa>
    {
        public EmpresaRepository(BancoDeDadosContext ctx) : base(ctx)
        {

        }
        public async Task<Empresa> GetByNomeAsync(string nome)
        {
            return await this._db.FirstOrDefaultAsync(p => p.Nome == nome);
        }

    }
}
