using Core.CMS.Data.Contexts;
using Core.CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Core.CMS.Data.Repositories
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
