using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Data.EF.Repositories
{
    public class ProdutoRepositoryEF : RepositoryEF<Produto>, IProdutoRepository
    {
        public ProdutoRepositoryEF(StoreDataContext ctx) : base(ctx) { }
        public async Task<Produto> GetByNomeAsync(string nome) => await _db.FirstOrDefaultAsync(p => p.Nome == nome);
        public async Task<IEnumerable<Produto>> GetWithCategoriaAsync() => await _db.Include(c => c.Categoria).ToListAsync();
    }
}
