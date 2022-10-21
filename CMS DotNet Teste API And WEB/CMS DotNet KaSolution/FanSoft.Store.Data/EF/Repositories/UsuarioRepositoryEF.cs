using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Entities;
using FanSoft.Sotre.Domain.Helpers;

namespace FanSoft.Store.Data.EF.Repositories
{
    public class UsuarioRepositoryEF : RepositoryEF<Usuario>, IUsuarioRepository
    {
        public UsuarioRepositoryEF(StoreDataContext ctx) : base(ctx) { }
        public async Task<Usuario> GetByNomeAsync(string nome) => await _db.FirstOrDefaultAsync(u => u.Nome == nome);
        public async Task<Usuario> AuthenticateAsync(string email, string senha) => await _db.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha.Encrypt());
    }
}
