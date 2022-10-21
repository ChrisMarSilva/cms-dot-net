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
    public class CategoriaRepositoryEF : RepositoryEF<Categoria>, ICategoriaRepository
    {
        public CategoriaRepositoryEF(StoreDataContext ctx) : base(ctx) { }
        public async Task<Categoria> GetByNomeAsync(string nome) => await _db.FirstOrDefaultAsync(p => p.Nome == nome);

    }
}
