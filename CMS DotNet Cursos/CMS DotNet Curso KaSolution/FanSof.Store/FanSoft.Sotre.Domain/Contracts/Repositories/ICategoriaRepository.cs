using FanSoft.Sotre.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Contracts.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> GetByNomeAsync(string nome);
    }
}
